
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using EMS.Areas.EMS.Extension;
using EMS.Models;
using OfficeOpenXml;

namespace EMS.Areas.EMS.Controllers
{
    public class CustomerEMSController : BaseController
    {
        private EMSEntities db = new EMSEntities();
        // GET: EMS/CustomerEMS
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public JsonResult List(string seach)
        {
            try
            {
                var customer = (from a in db.Customers.Where(x => x.Id.Length > 0)
                                select new
                                {
                                    id = a.Id,
                                    name = a.Name,
                                    phone = a.Phone,
                                    email = a.Email,
                                    idTypeCustomer = a.TypeCustomer.Name == null?"": a.TypeCustomer.Name,
                                }).ToList().Where(x=>x.name.ToLower().Contains(seach)|| x.name.Contains(seach)
                                                    ||x.phone.ToString().Contains(seach)
                                                    ||x.email.ToLower().Contains(seach)||x.email.Contains(seach)
                                                    ||x.idTypeCustomer.ToLower().Contains(seach)||x.idTypeCustomer.Contains(seach));
                return Json(new { code = 200, customer =customer,msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Deletes(string id)
        {
            try
            {
                var customer = db.Customers.SingleOrDefault(x => x.Id == id);
                var attendEventCount = db.AttendEvents.Where(x => x.IdCustomer == id);
                var personVotedCount = db.PersonVoteds.Where(x => x.IdCustomer == id);
                for(int i = 0; i < attendEventCount.Count(); i++)
                {
                    var idAttendEvent = attendEventCount.ToList().LastOrDefault().Id;
                    var attendEvent = db.AttendEvents.SingleOrDefault(x => x.Id == idAttendEvent);
                    db.AttendEvents.Remove(attendEvent);
                    db.SaveChanges();
                }
                for (int i = 0; i < personVotedCount.Count(); i++)
                {
                    var idPersonVoted = personVotedCount.ToList().LastOrDefault().Id;
                    var personVoted = db.PersonVoteds.SingleOrDefault(x => x.Id == idPersonVoted);
                    db.PersonVoteds.Remove(personVoted);
                    db.SaveChanges();
                }
                db.Customers.Remove(customer);
                db.SaveChanges();
                return Json(new { code = 200,  msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Adds(string name, int phone , string email,int idTypeCustomer)
        {
            try
            {
                var d = DateTime.Now;
                var id = d.Day +""+ d.Month + "" + d.Year + "" + d.Minute + "" + d.Second + "" + d.Millisecond;
                Customer customer = new Customer();
                customer.Id = id;
                if (idTypeCustomer == 0)
                {
                    customer.IdTypeCustomer = null;
                }
                else
                {
                    customer.IdTypeCustomer = idTypeCustomer;
                }
               
                customer.Name = name;
                customer.Phone = phone;
                customer.Email = email;
                customer.Status = true;
                db.Customers.Add(customer);
                db.SaveChanges();
                EmailExtension.SendNotificationEmail(customer);
                return Json(new { code = 200, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Upload(FormCollection formCollection)
        {
            var session = (Admin)Session["admin"];
            var nameAdmin = session.Name;
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        ExcelWorksheet currentSheet = package.Workbook.Worksheets.First();
                        var workSheet = currentSheet;
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                            var d = DateTime.Now;
                            Random r = new Random();
                            var id = d.Day + "" + d.Month + "" + d.Year + "" + d.Minute + "" + d.Second + "" + d.Millisecond + "" + r.Next(0, 9);
                            Customer customer = new Customer();
                            var idTypeCustomer = workSheet.Cells[rowIterator, 3].Value == null ? "" : workSheet.Cells[rowIterator, 3].Value.ToString();
                            var name = workSheet.Cells[rowIterator, 1].Value.ToString();
                            var phone = int.Parse(workSheet.Cells[rowIterator, 2].Value.ToString());
                            var email = workSheet.Cells[rowIterator, 4].Value.ToString();
                            var checkCustomer = db.Customers.SingleOrDefault(x => x.Id == id);
                            if (name.Length == 0)
                            {
                                MessageBox.Show("Chưa Nhập Tên ở Dòng" + rowIterator);
                                return View("Index");
                            }
                            if (phone < 0)
                            {
                                MessageBox.Show("Chưa Nhập SĐT ở Dòng" + rowIterator);
                                return View("Index");
                            }
                            if (email.Length == 0)
                            {
                                MessageBox.Show("Chưa Nhập Email ở Dòng" + rowIterator);
                                return View("Index");
                            }
                            if (checkCustomer == null)
                            {
                                customer.Id = id;
                                if (idTypeCustomer == "")
                                {
                                    customer.IdTypeCustomer = null;
                                }
                                else
                                {
                                    customer.IdTypeCustomer = int.Parse(idTypeCustomer);
                                }

                                customer.Name = name;
                                customer.Phone = phone;
                                customer.Email = email;
                                customer.Status = true;
                                db.Customers.Add(customer);
                                db.SaveChanges();
                                EmailExtension.SendNotificationEmail(customer);
                            }
                            else
                            {
                                MessageBox.Show("Lỗi tại dòng" + rowIterator);
                                return View("Index");
                            }
                            Thread.Sleep(200);
                        }
                    }
                }
            }

            return View("Index");
        }
    }
}