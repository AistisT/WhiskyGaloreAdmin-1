using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WhiskyGaloreAdmin.Filters
{
    public class AdminFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (System.Web.HttpContext.Current.Session["account"] == null)
            {

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary 
                { 
                    { "controller", "Home" }, 
                    { "action", "Login" } 
                });
            }
            else if (System.Web.HttpContext.Current.Session["account"].ToString() != "Administrator")
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary 
                { 
                    { "controller", "Error" }, 
                    { "action", "RestrictedPage" } 
                });
            }
        }

    }


}
