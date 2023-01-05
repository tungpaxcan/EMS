using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Models;

namespace EMS.Controllers
{
    public class BaseHomeController : Controller
    {
        private EMSEntities db = new EMSEntities();
        // GET: BaseHome
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["user"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new { controller = "DefaultHome", action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}