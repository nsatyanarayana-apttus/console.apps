using Akka.Actor;
using Apttus.OpenTracingTelemetry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceB.Service;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ServiceB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private IApttusOpenTracer Tracer;
        private IServiceA ServiceA;
        private IWebActorService WebActorService;

        public DemoController(IApttusOpenTracer tracer, IServiceA servicea, IWebActorService webActorservice)
        {
            Tracer = tracer;
            ServiceA = servicea;
            WebActorService = webActorservice;
        }


        [HttpGet]
        [Route("test1")]
        public async Task<string> Get()
        {
            
            using (Tracer.BuildActiveSpan("DemoController-Get-" + HttpContext.TraceIdentifier,true))
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

            using (Tracer.BuildActiveSpan("GetTest2-ActiveSpan1-" + HttpContext.TraceIdentifier,true))
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

            
            using (Tracer.BuildActiveSpan("GetTest2-ActiveSpan1-" + HttpContext.TraceIdentifier,true))
            {
                Tracer.ActiveSpan.Log("Logged in controller");
                //await ServiceA.GetServiceAMessageTest3Async(HttpContext.TraceIdentifier);
                await WebActorService.Ask<string>("hello world");
            }

            Tracer.ActiveSpan.Log("Outside the Span in DemoController");
            return "GetTest2";
        }

        [HttpGet]
        [Route("test4")]
        public async Task<string> GetTest4()
        {


            using (Tracer.BuildActiveSpan("GetTest2-ActiveSpan1-" + HttpContext.TraceIdentifier, true))
            {
                Tracer.ActiveSpan.Log("Logged in controller");
                await ServiceA.ProcessUsingThreadPool(HttpContext.TraceIdentifier);
            }

            Tracer.ActiveSpan.Log("Outside the Span in DemoController");
            return "GetTest2";
        }

        [HttpGet]
        [Route("test5")]
        public async Task<string> GetTest5()
        {
            using (Tracer.BuildActiveSpan("GetTest2-ActiveSpan1-" + HttpContext.TraceIdentifier, true))
            {
                Tracer.ActiveSpan.Log("Logged in controller");
                await ServiceA.SendToSockerServer(HttpContext.TraceIdentifier);
            }

            Tracer.ActiveSpan.Log("Outside the Span in DemoController");
            return "GetTest5";
        }

       
    }
}
