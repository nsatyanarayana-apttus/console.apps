using Akka.Actor;
using OpenTracing;
using System;
using System.Threading.Tasks;

namespace ServiceA.Actor
{
    public class WebActor : ReceiveActor
    {
        private ITracer Tracer;

        public WebActor(ITracer tracer)
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
