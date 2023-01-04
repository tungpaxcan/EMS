using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Models;

namespace EMS.Areas.EMS.Controllers
{
    public class AttendEventEMSController : Controller
    {
        private EMSEntities db = new EMSEntities();
        // GET: EMS/AttendEventEMS
        [HttpPost]
        public JsonResult Adds(string id)
        {
            try
            {
                var checkAttendEvent = db.AttendEvents.SingleOrDefault(x => x.IdCustomer == id);
                if (checkAttendEvent == null)
                {
                    AttendEvent attendEvent = new AttendEvent();
                    attendEvent.IdCustomer = id;
                    attendEvent.Status = true;
                    db.AttendEvents.Add(attendEvent);
                    db.SaveChanges();
                    return Json(new { code = 200, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { code = 100, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}