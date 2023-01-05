using EMS.Areas.EMS.Extension;
using EMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.Controllers
{
    public class CustomerQRController : Controller
    {
        private EMSEntities db = new EMSEntities();
        // GET: CustomerQR
        public ActionResult Index(string ID="")
        {
           if(db.Customers.Any(c => c.Id.Equals(ID)))
           {
                ViewBag.CusID = ID;
           }
           
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SendQR(string ID = "", string SVG = "")
        {
            var cus = db.Customers.Find(ID);
            EmailExtension.SendQREmail(cus, SVG);
            return Json("OK");
        }
    }
}