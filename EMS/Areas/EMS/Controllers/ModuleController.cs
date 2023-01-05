using EMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.Areas.EMS.Controllers
{
    public class ModuleController : Controller
    {
        private EMSEntities db = new EMSEntities();
        // GET: EMS/Module
        public ActionResult Index()
        {
            return View();
        }

        // GET: EMS/Module/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EMS/Module/Create
        public ActionResult Create()
        {
            var module = db.Modules.FirstOrDefault();
            return View(module);
        }

        // POST: EMS/Module/Create
        [HttpPost]
        public ActionResult SettingModule(FormCollection collection)
        {
            try
            {
                int id = Int16.Parse(collection["ModuleID"]);
                var module = db.Modules.Find(id);
                // TODO: Add insert logic here
                string logo = collection["Logo"].Equals("") ? module.Logo : collection["Logo"].Trim();
                string image = collection["Image"].Equals("") ? module.Image : collection["Image"].Trim();
                module.Name = collection["name"].Trim();
                module.Des = collection["Des"].Trim();
                module.Link = collection["Link"].Trim();
                module.Image = image;
                module.Logo = logo;
                module.Invitation = collection["Invitation"].Trim();
                module.Content = collection["Content"].Trim();
                module.CreateDate = DateTime.Now;
                db.SaveChanges();
                return Json(
                            new
                            {
                                status = "200",

                            }
                            , JsonRequestBehavior.AllowGet
                             );
            }
            catch (Exception ex)
            {
                return Json(
                              new
                              {
                                  status = "500",
                                  message = ex.Message,

                              }
                              , JsonRequestBehavior.AllowGet
                               );
            }
        }



        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    List<string> imageName = new List<string>();
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                       
                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Images/"), fname);
                        file.SaveAs(fname);
                        imageName.Add(file.FileName);
                    }
                    // Returns message that successfully uploaded  
                    return Json(
                            new
                            {
                                status = "200",

                                imgNames = imageName.ToArray()
                            }
                            , JsonRequestBehavior.AllowGet
                             ) ;
                }
                catch (Exception ex)
                {
                    return Json(
                             new
                             {
                                 status = "500",
                                 message = ex.Message,
                                
                             }
                             , JsonRequestBehavior.AllowGet
                              );
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        [HttpGet]
        
        // GET: EMS/Module/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EMS/Module/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EMS/Module/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EMS/Module/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
