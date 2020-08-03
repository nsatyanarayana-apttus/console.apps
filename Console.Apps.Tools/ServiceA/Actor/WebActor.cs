using Akka.Actor;
using OpenTracing;

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
        }
    }
}
