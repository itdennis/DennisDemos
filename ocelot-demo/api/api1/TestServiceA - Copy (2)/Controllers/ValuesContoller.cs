﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestServiceA.Controllers
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
            return new string[] { "value1A", "value2A" };
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

        
    }
}
