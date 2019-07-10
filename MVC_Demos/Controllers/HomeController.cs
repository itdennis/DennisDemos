using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demos.Controllers
{
    public class HomeController : System.Web.Mvc.Controller
    {
        [Route("post")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetImg()
        {
            var avatarBytes = System.IO.File.ReadAllBytes(@"C:\Users\v-yanywu\source\repos\Dobi\Dobi\Dobi\wwwroot\images\avatar-placeholder.png");
            FileContentResult result = File(avatarBytes, "image/png");
            return result;
        }
    }
}