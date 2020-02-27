using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TestServiceB.Controllers
{
    [Route("apiservice/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        public ValuesController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1B", "value2B" };
        }

        // GET api/values/5
        [HttpGet]//("{id}")
        public ActionResult<string> Getid(int id)
        {
            return HttpContext.Request.Host.Port + "=>" + Configuration["AppName"] + "=>" + DateTime.Now.ToString() + "=>" + id;
        }

        // POST api/values
        [HttpPost]
        public string Post(string value)
        {
            return HttpContext.Request.Host.Port + "=>" + Configuration["AppName"] + "=>" + DateTime.Now.ToString() + "=>" + value;
        }

        [HttpGet]
        public IActionResult Heathle()
        {
            return Ok();
        }
    }
}