using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace consul.service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConsulController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetService() 
        {
            using (ConsulClient client = new ConsulClient(c=>c.Address = new Uri("http://localhost:8500/")))
            {
                var services = client.Agent.Services().Result.Response;
            }

            return Ok("666");
        }
    }
}