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
        [Route("test1")]
        public async Task<string> Get()
        {
            
            using (Tracer.BuildSpan("DemoController-Get-" + HttpContext.TraceIdentifier).StartActive(true))
            {
                Tracer.ActiveSpan.Log("Logged in controller");
                await ServiceA.GetServiceAMessageTest1Async(HttpContext.TraceIdentifier);
            }

            Tracer.ActiveSpan.Log("Outside the Span in DemoController");
            return "Hello World";
        }

        [HttpGet]
        [Route("test2")]
        public async Task<string> GetTest2()
        {

            using (Tracer.BuildSpan("GetTest2-ActiveSpan1-" + HttpContext.TraceIdentifier).StartActive(true))
            {
                Tracer.ActiveSpan.Log("Logged in controller");
                await ServiceA.GetServiceAMessageTest2Async(HttpContext.TraceIdentifier);
            }

            Tracer.ActiveSpan.Log("Outside the Span in DemoController");
            return "GetTest2";
        }

        [HttpGet]
        [Route("test3")]
        public async Task<string> GetTest3()
        {

            using (Tracer.BuildSpan("GetTest2-ActiveSpan1-" + HttpContext.TraceIdentifier).StartActive(true))
            {
                Tracer.ActiveSpan.Log("Logged in controller");
                await ServiceA.GetServiceAMessageTest3Async(HttpContext.TraceIdentifier);
            }

            Tracer.ActiveSpan.Log("Outside the Span in DemoController");
            return "GetTest2";
        }
    }
}
