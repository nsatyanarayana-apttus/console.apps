using Akka.Actor;
using Apttus.OpenTracingTelemetry;
using OpenTracing;
using System;
using System.Threading.Tasks;

namespace ServiceB.Actor
{
    public class WebActor : ReceiveActor
    {
        private IApttusOpenTracer Tracer;

        public WebActor(IApttusOpenTracer tracer)
        {
            Tracer = tracer;
            Receive<string>(msg =>
            {
                int hashcode = Tracer.GetHashCode();
                Tracer.ActiveSpan?.Log(msg + hashcode);
                Sender.Tell("done");
            });

            Receive<int>(msg =>
            {
                int hashcode = Tracer.GetHashCode();
                using (Tracer.BuildSpan("WebActor-ActiveSpan6-" + msg).StartActive(true))
                {
                    Task.Delay(TimeSpan.FromSeconds(5)).GetAwaiter().GetResult();
                }
                Tracer.ActiveSpan?.Log("WebActor-ActiveSpan6");
                Sender.Tell(hashcode);
            });
        }
    }
}
