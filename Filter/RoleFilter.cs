using Atlas.Models.AltasModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Atlas.Filter
{
    public class RoleFilter
    {

        //Role_Impl
        public class RoleAuthorizeAttribute : AuthorizeAttribute
        {
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {

                var theUser = httpContext.Session["user_id"];
                var theUserType = httpContext.Session["user_type"];
                if (theUser != null && theUserType != null)
                {
                    return true;
                }
                httpContext.Session.Abandon();
                return false;
            }

            protected override void HandleUnauthorizedRequest(AuthorizationContext context)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" })); 
                
            }
        }
        ///Role_Impl


        public class UserAuthorizeAttribute : AuthorizeAttribute
        {

            private dbContext db = new dbContext();
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                var theUser = httpContext.Session["user_id"];
                var theUserType = httpContext.Session["user_id"];
                if(theUser != null)
                {
                    var person = db.Users.Where(u => u.ApplicationUserId == theUser.ToString()).FirstOrDefault();
                    if (theUser != null && theUserType != null && person.UserTypeID != 2)
                        return false;
                    else
                        return true;
                }
                return true;
               
            }

            protected override void HandleUnauthorizedRequest(AuthorizationContext context)
            {
                UrlHelper urlHelper = new UrlHelper(context.RequestContext);
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Meetings" }));
            }
        }
    }
}