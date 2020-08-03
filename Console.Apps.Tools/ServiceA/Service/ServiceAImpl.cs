using OpenTracing;
using System;
using System.Threading.Tasks;

namespace ServiceA.Service
{
    public class ServiceAImpl : IServiceA
    {
        private ITracer Tracer;

        public ServiceAImpl(ITracer tracer)
        {
            Tracer = tracer;
        }

        public async Task<string> GetServiceAMessageAsync(string id)
        {

            using (Tracer.BuildSpan("ServiceAImpl-" + id).StartActive(true))
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                Tracer.ActiveSpan.Log("Logged in controller");
            }

            Tracer.ActiveSpan.Log("Out side Using Blocks -" + id);
            return "Waited";
        }
    }
}
