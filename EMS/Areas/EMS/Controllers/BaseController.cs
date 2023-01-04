using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Models;

namespace EMS.Areas.EMS.Controllers
{
    public class BaseController : Controller
    {
        private EMSEntities db = new EMSEntities();
        // GET: EMS/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["admin"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new { controller = "Default", action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }
        [HttpGet]
        public JsonResult SignOut()
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                Session["admin"] = null;
                return Json(new { code = 200, }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Sai !!!" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}