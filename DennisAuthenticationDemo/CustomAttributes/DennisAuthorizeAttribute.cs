using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DennisAuthenticationDemo.CustomAttributes
{
    public class DennisAuthorizeAttribute : AuthorizeAttribute
    {
        private bool localAllowed;
        public DennisAuthorizeAttribute(bool allowedParam = true)
        {
            localAllowed = allowedParam;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsLocal)
            {
                return localAllowed;
            }
            else
            {
                return true;
            }
        }
    }
}