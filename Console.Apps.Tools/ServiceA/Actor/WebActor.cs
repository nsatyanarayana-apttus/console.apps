using Akka.Actor;
using Apttus.OpenTracingTelemetry;
using System;
using System.Threading.Tasks;

namespace ServiceB.Actor
{
    public class WebActor : ReceiveActor
    {
        public  IApttusOpenTracer Tracer;

        public static IApttusOpenTracer Tracer1;

        public WebActor(IApttusOpenTracer tracer)
        {
            Tracer = tracer;
           // Tracer1 = tracer;
            Receive<string>(msg =>
            {
                int hashcode = Tracer.GetHashCode();
                Tracer.ActiveSpan?.Log(msg + hashcode);
                Sender.Tell("done");
            });

            Receive<int>(msg =>
            {
                //int hashcode = (int)ApttusGlobalTracer.Current?.GetHashCode();
                using (Tracer.BuildActiveSpan("WebActor-ActiveSpan6-" + msg,true))
                {
                    Task.Delay(TimeSpan.FromSeconds(5)).GetAwaiter().GetResult();
                }
                Tracer.ActiveSpan?.Log("WebActor-ActiveSpan6");
                Sender.Tell(12345);
            });
        }
    }
}
