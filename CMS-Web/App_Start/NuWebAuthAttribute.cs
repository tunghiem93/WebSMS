using CMS_DTO.CMSCustomer;
using CMS_DTO.CMSSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CMS_Web.Web.App_Start
{
    public class NuWebAuthAttribute : AuthorizeAttribute
    {
        private CustomerModels _CurrentUser;

        private string Controller;
        private string Action;

        /*Factory*/

        public NuWebAuthAttribute()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.Session["UserC"] == null)
                _CurrentUser = new CustomerModels();
            else
                _CurrentUser = (CustomerModels)HttpContext.Current.Session["UserC"];
                        
            //Alias Controller //Action
            Controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            Action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString().ToLower();

            return IsPermission();
        }

        protected bool IsPermission()
        {
            try
            {
                // If user not logged in, require login
                if (!_CurrentUser.IsAuthenticated)
                    return false;
                else
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                string error = e.ToString();
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!_CurrentUser.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Home",
                            action = "Index",
                        })
                    );
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Error",
                            action = "Unauthorised",
                            area = string.Empty,
                        })
                    );
            }
        }
    }
}