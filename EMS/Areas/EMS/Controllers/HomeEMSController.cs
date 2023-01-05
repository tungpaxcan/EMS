using EMS.Areas.EMS.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.Areas.EMS.Controllers
{
    public class HomeEMSController : BaseController
    {
        // GET: EMS/HomeEMS
        public ActionResult Index()
        {
            //EmailExtension.SendNotificationEmail("hoanghgggg@gmail.com");
            return View();
           
        }
    }
}