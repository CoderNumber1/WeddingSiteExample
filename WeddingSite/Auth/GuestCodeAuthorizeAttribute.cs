using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WeddingSite.Models;

namespace WeddingSite.Auth
{
    public class GuestCodeAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = base.AuthorizeCore(httpContext);

            if(!authorize)
            {
                return authorize;
            }

            var identity = Thread.CurrentPrincipal as ClaimsPrincipal;

            if (httpContext.Session["GuestCodeValidated"] != null && (bool)httpContext.Session["GuestCodeValidated"])
            {
                authorize = true;
            }
            else if (identity.HasClaim(c => c.Type == "GuestCode"))
            {
                var db = new WeddingManagementContext();

                string guestCode = identity.FindFirst("GuestCode").Value.ToUpper();

                authorize = db.GuestCodes.Any(gc => gc.GuestCode1.ToUpper() == guestCode && gc.UseLimit > 0);

                if (authorize)
                {
                    httpContext.Session["GuestCodeValidated"] = true;
                }
            }
            else
            {
                var db = new WeddingManagementContext();
                int userId = HttpContext.Current.User.Identity.GetUserId<int>();

                UserClaim guestCodeClaim = db.Users.FirstOrDefault(u => u.Id == userId).Claims.FirstOrDefault(c => c.ClaimType == "GuestCode");

                if (guestCodeClaim != null)
                {
                    authorize = db.GuestCodes.Any(gc => gc.GuestCode1.ToUpper() == guestCodeClaim.ClaimValue.ToUpper() && gc.UseLimit > 0);
                }

                if (authorize)
                {
                    httpContext.Session["GuestCodeValidated"] = true;
                }
            }

            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Session["RegisterGuestReturn"] = filterContext.HttpContext.Request.RawUrl;

            filterContext.Result = new RedirectToRouteResult(
                                   new RouteValueDictionary
                                   {
                                       { "action", "RegisterGuest" },
                                       { "controller", "Guest" }
                                   });
        }
    }
}