using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Models;

namespace EMS.Areas.EMS.Controllers
{
    public class DefaultController : Controller
    {
        private EMSEntities db = new EMSEntities();
        // GET: EMS/Default
        public ActionResult Login()
        {
            if (Session["admin"] != null)
            {
                return RedirectToAction("Index", "HomeEMS");
            }
            return View();
        }

        [HttpPost]
        public JsonResult Login(string user, string pass)
        {
            try
            {
                var passU = Encode.ToMD5(pass);
                var acc = db.Admins.SingleOrDefault(x => x.UserName == user && x.PassWord == passU);
                if (acc != null)
                {
                    Session["admin"] = acc;
                    return Json(new { code = 100, Url = "/EMS/HomeEMS/Index/" }, JsonRequestBehavior.AllowGet);
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