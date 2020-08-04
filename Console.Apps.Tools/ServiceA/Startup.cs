using System.Net.Http;
using Jaeger;
using Jaeger.Samplers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTracing;
using OpenTracing.Util;
using ServiceB.Service;
using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Akka.Configuration;
using OurFile = System.IO;
using Akka.Actor;
using ServiceB.Actor;
using Akka.DI.Core;
using Apttus.OpenTracingTelemetry.AspNetCore.Extensions;
using Apttus.FeatureFlag.Interface;
using Apttus.FeatureFlag.Implementation;
using System.Collections.Generic;
using Apttus.OpenTracingTelemetry;
using Microsoft.AspNetCore.Http;
using ServiceA.Service;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ServiceB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private string SERVICE_NAME = "Tracer TestService";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            if (Configuration.GetSection("FeatureFlag") != null)
            {
                var apiKey = Configuration.GetValue<string>("FeatureFlag:ApiKey");

                services.AddSingleton<IFeatureFlag>(e => { return new LaunchDarklyFeatureFlag(apiKey); });
            }

            services.AddOpenTracing(SERVICE_NAME, Configuration);
            services.AddTransient<HttpClient>();

            // Adds the Jaeger Tracer.
            //services.AddSingleton<ITracer>(serviceProvider =>
            //{
            //    string serviceName = serviceProvider.GetRequiredService<IHostingEnvironment>().ApplicationName;

            //    // This will log to a default localhost installation of Jaeger.
            //    var tracer = new Tracer.Builder(serviceName)
            //        .WithSampler(new ConstSampler(true))
            //        .Build();

            //    // Allows code that can't use DI to also access the tracer.
            //    GlobalTracer.Register(tracer);

            //    return tracer;
            //});

            //// Prevent endless loops when OpenTracing is tracking HTTP requests to Jaeger.
            //services.Configure<HttpHandlerDiagnosticOptions>(options =>
            //{
            //    options.IgnorePatterns.Add(x => !x.RequestUri.IsLoopback);
            //});

            AddSocketServer();
            services.AddCustomServices();
            
            return ConfigureActor(services);
        }

        public IServiceProvider ConfigureActor(IServiceCollection services)
        {
            // Create and build container
            var builder = new ContainerBuilder();
            services.AddAutofac();

            builder.Populate(services);

            var configuration = ConfigurationFactory.ParseString(OurFile.File.ReadAllText("actor.conf"));
            var customActorSystem = ActorSystem.Create("DemoActor", configuration);

            var lazyActorSys = new Lazy<ActorSystem>(() => { return customActorSystem; });
            builder.Register<ActorSystem>(x => lazyActorSys.Value).SingleInstance();
            builder.RegisterType<WebActor>();

            builder.RegisterType<WebActorService>().OnActivating((e) => {
                var actorSys = e.Context.Resolve<ActorSystem>();
                var webActorProps = actorSys.DI().Props<WebActor>();
                var actorRef = actorSys.ActorOf(webActorProps);
                e.ReplaceInstance(new WebActorService(actorRef));
            }).As<IWebActorService>().SingleInstance();

            var container = builder.Build();
            customActorSystem.UseAutofac(container);

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseApttusMonitoring(SERVICE_NAME, AdditionalRequestSpanInfo);
            app.UseMvc();
        }

        private Dictionary<string, string> AdditionalRequestSpanInfo(HttpContext context)
        {
            var respDict = new Dictionary<string, string>();

            if (context != null && context.Request != null && context.Request.Headers != null)
            {
                if (context.Request.Headers.ContainsKey(Constant.RequestId))
                {
                    respDict.Add(Constant.RequestId, context.Request.Headers[Constant.RequestId]);
                }

                if (context.Request.Headers.ContainsKey(Constant.AppId))
                {
                    respDict.Add(Constant.AppId, context.Request.Headers[Constant.AppId]);
                }

                if (context.Request.Headers.ContainsKey(Constant.TenantId))
                {
                    respDict.Add(Constant.TenantId, context.Request.Headers[Constant.TenantId]);
                }
            }

            return respDict;
        }

        public  void AddSocketServer()
        {
            

            Task.Run(() => {

                TcpListener server = new TcpListener(System.Net.IPAddress.Any, 5053);
                //Start the server
                server.Start();
                Console.WriteLine("Server started. Waiting for connection...");
                String str = "";
                do
                {
                    //Block execution until a new client is connected.
                    TcpClient newClient = server.AcceptTcpClient();

                    Console.WriteLine("New client connected!");

                    int hashcode = (int)ApttusGlobalTracer.Current?.GetHashCode();
                    //Checking if new data is available to be read on the network stream
                    if (newClient.Available > 0)
                    {
                        //Initializing a new byte array the size of the available bytes on the network stream
                        byte[] readBytes = new byte[newClient.Available];
                        //Reading data from the stream
                        newClient.GetStream().Read(readBytes, 0, newClient.Available);
                        //Converting the byte array to string
                        str = System.Text.Encoding.ASCII.GetString(readBytes);
                        //This should output "Hello world" to the console window
                        Console.WriteLine(str);
                    }
                } while (!string.IsNullOrEmpty(str));

            });
        }
    }
}
