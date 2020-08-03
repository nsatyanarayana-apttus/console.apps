using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using ServiceA.Service;
using System.Threading.Tasks;

namespace ServiceA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private ITracer Tracer;
        private IServiceA ServiceA;

        public DemoController(ITracer tracer, IServiceA servicea)
        {
            Tracer = tracer;
            ServiceA = servicea;
        }


        [HttpGet]
        public async Task<string> Get()
        {
            
            using (Tracer.BuildSpan("DemoController-"+ HttpContext.TraceIdentifier).StartActive(true))
            {
                Tracer.ActiveSpan.Log("Logged in controller");
                await ServiceA.GetServiceAMessageAsync(HttpContext.TraceIdentifier);
            }

            return "Hello World";
        }
    }
}
