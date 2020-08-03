using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ServiceA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AworldController : ControllerBase
    {
        //https://localhost:44340/api/aworld
        // GET api/aworld
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Hello", "ServiceA - Aworld" };
        }

        // GET api/aworld/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in Request.Headers.Keys) {
                sb.AppendLine($"{key} : {Request.Headers[key]}");
            }

            string ss = sb.ToString();

            return "Hello World";
        }



    }
}
