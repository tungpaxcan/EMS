using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Models;

namespace EMS.Controllers
{
    public class DefaultHomeController : Controller
    {
        private EMSEntities db = new EMSEntities();
        // GET: DefaultHome
        public ActionResult Login()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public JsonResult Login(string qr)
        {
            try
            {
                var acc = db.Customers.SingleOrDefault(x => x.Id == qr);
                if (acc != null)
                {
                    Session["user"] = acc;
                    return Json(new { code = 100, Url = "/Home/Index/" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { code = 300, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}