using Akka.Actor;
using OpenTracing;

namespace ServiceA.Actor
{
    public class WebActor : ReceiveActor
    {
        private ITracer Tracer;

        public WebActor(ITracer tracer)
        {

            Receive<string>(msg =>
            {
                Tracer.ActiveSpan.Log(msg);
            });
        }
    }
}
