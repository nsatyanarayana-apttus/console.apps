using Akka.Actor;
using Apttus.OpenTracingTelemetry;
using ServiceB.Actor;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceB.Service
{
    public class WebActorService : IWebActorService
    {
        private readonly IActorRef webActorRef;

        public WebActorService()
        {

        }

        public WebActorService(IActorRef webActor)
        {
            this.webActorRef = webActor;
        }

        public Task<T> Ask<T>(object message, TimeSpan? timeout = null)
        {
            bool f1 = ExecutionContext.IsFlowSuppressed();
            //int hashcode = (int)WebActor.Tracer1?.ActiveSpan?.GetHashCode();
            //int hashcode = (int)WebActor.Tracer1?.ActiveSpan?.GetHashCode();
            var task1= webActorRef.Ask<T>(message, timeout);
            // hashcode = (int)ApttusGlobalTracer.Current?.GetHashCode();
            //ApttusGlobalTracer.Current?.ActiveSpan?.Log("ggggg");
            //return Task.FromResult<T>(default(T));
            return task1;
        }
    }
}
