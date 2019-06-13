using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DennisWebDemos.Controllers
{
    [Authorize] // Require authenticated requests.
    public class AuthorizeOnControllerController : Controller
    {
        // GET: AuthorizeOnController
        public string Index()
        {
            return "this is index action in AuthorizeOnControllerController";
        }
    }
}