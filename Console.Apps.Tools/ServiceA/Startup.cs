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
using OpenTracing.Contrib.NetCore.CoreFx;
using ServiceB.Service;
using System;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Akka.Configuration;
using OurFile = System.IO;
using Akka.Actor;
using ServiceB.Actor;
using System;
using Akka.DI.Core;

namespace ServiceB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddOpenTracing();
            services.AddTransient<HttpClient>();

            // Adds the Jaeger Tracer.
            services.AddSingleton<ITracer>(serviceProvider =>
            {
                string serviceName = serviceProvider.GetRequiredService<IHostingEnvironment>().ApplicationName;

                // This will log to a default localhost installation of Jaeger.
                var tracer = new Tracer.Builder(serviceName)
                    .WithSampler(new ConstSampler(true))
                    .Build();

                // Allows code that can't use DI to also access the tracer.
                GlobalTracer.Register(tracer);

                return tracer;
            });

            // Prevent endless loops when OpenTracing is tracking HTTP requests to Jaeger.
            services.Configure<HttpHandlerDiagnosticOptions>(options =>
            {
                options.IgnorePatterns.Add(x => !x.RequestUri.IsLoopback);
            });

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
            app.UseMvc();
        }
    }
}
