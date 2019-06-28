using DennisAuthenticationDemo.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DennisAuthenticationDemo.Controllers
{
    [DennisActionFilter]
    [Authorize]
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetBlogById()
        {
            return View();
        }
    }
}