using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private ITracer Tracer;

        public DemoController(ITracer tracer)
        {
            Tracer = tracer;
        }


        [HttpGet]
        public string Get()
        {
            
            using (Tracer.BuildSpan("DemoController-"+ HttpContext.TraceIdentifier).StartActive(true))
            {
                Tracer.ActiveSpan.Log("Logged in controller");
            }

            return "Hello World";
        }
    }
}
