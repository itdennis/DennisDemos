using DennisAuthenticationDemo.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DennisAuthenticationDemo.Controllers
{
    [DennisAuthFilter]
    [DennisActionFilter]
    public class HomeController : Controller
    {
        [DennisAuthorizeAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [DennisAuthorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize(Users ="dobi@dobi.com")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}