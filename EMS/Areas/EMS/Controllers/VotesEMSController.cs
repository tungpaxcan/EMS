using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Models;

namespace EMS.Areas.EMS.Controllers
{
    public class VotesEMSController : BaseController
    {
        private EMSEntities db = new EMSEntities();
        // GET: EMS/VotesEMS
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Win(int voteNews)
        {
            try
            {
                var winVoted = (from a in db.Votes.Where(x => x.PersonVoted.VoteNew.Id == voteNews)
                                select new { 
                                    id = a.IdPersonVoted,
                                    name = a.PersonVoted.Name
                                }).ToList();

                return Json(new { code = 200, winVoted = winVoted, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult WinName(int item)
        {
            try
            {
                var personVoted = db.PersonVoteds.SingleOrDefault(x => x.Id == item).Customer.Name;

                return Json(new { code = 200, personVoted = personVoted, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}