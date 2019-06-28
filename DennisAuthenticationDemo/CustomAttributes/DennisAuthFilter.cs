using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace DennisAuthenticationDemo.CustomAttributes
{
    public class DennisAuthFilter : FilterAttribute, IAuthenticationFilter
    {
        private bool _auth;
        /// <summary>
        /// 首先执行OnAuthentication方法，该方法可用于执行任何所需的身份验证。
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //Logic for authenticating a user    
            _auth = (filterContext.ActionDescriptor.GetCustomAttributes(typeof(OverrideAuthenticationAttribute), true)).Length == 0;
        }
        /// <summary>
        ///  OnAuthenticationChallengemethod用于根据经过身份验证的用户的主体来限制访问。
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            //TODO: Additional tasks on the request    
            var user = filterContext.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}