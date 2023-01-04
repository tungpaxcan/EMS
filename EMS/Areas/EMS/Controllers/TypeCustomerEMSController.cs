using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Models;

namespace EMS.Areas.EMS.Controllers
{
    public class TypeCustomerEMSController : BaseController
    {
        private EMSEntities db = new EMSEntities();
        // GET: EMS/TypeCustomerEMS
        public ActionResult Index()
        {
            return View(db.TypeCustomers.Where(x=>x.Id>0));
        }
        [HttpGet]
        public JsonResult List()
        {
            try
            {
                var typeCustomer = (from a in db.TypeCustomers.Where(x => x.Id > 0)
                                    select new
                                    {
                                        id = a.Id,
                                        name = a.Name
                                    }).ToList();
                return Json(new { code = 200, typeCustomer= typeCustomer, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Adds(string name)
        {
            try
            {
                TypeCustomer typeCustomer = new TypeCustomer();
                typeCustomer.Name = name;
                db.TypeCustomers.Add(typeCustomer);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Deletes(int id)
        {
            try
            {
                TypeCustomer typeCustomer = db.TypeCustomers.SingleOrDefault(x => x.Id== id);
                db.TypeCustomers.Remove(typeCustomer);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}