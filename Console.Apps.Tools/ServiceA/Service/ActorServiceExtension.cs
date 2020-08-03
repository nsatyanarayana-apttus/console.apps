using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Akka.Configuration;
using OurFile = System.IO;
using Akka.Actor;
using ServiceA.Actor;
using System;
using Akka.DI.Core;

namespace ServiceA.Service
{
    public static class ActorServiceExtension
    {
        public static IServiceCollection AddActorServices(this IServiceCollection services)
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

            return services;
        }
    }
}
