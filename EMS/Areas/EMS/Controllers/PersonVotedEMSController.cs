using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Models;

namespace EMS.Areas.EMS.Controllers
{
    public class PersonVotedEMSController : Controller
    {
        private EMSEntities db = new EMSEntities();
        // GET: EMS/PersonVotedEMS
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult List(int idVoteNews)
        {
            try
            {
                var personVoted = (from a in db.PersonVoteds.Where(x => x.IdVoteNews == idVoteNews)
                                   select new
                                   {
                                       id = a.Id,
                                       name = a.Customer.Name,
                                       email = a.Customer.Email
                                   }).ToList();
                var voteNews = db.VoteNews.SingleOrDefault(x => x.Id == idVoteNews).Status;
                return Json(new { code = 200,personVoted=personVoted, voteNews= voteNews, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Adds(string idCustomer)
        {
            try
            {
                PersonVoted personVoted = new PersonVoted();
                var idVoteNews = db.VoteNews.OrderBy(x => x.Id).ToList().LastOrDefault();
                personVoted.IdVoteNews = idVoteNews.Id;
                personVoted.IdCustomer = idCustomer;
                personVoted.Status = true;
                db.PersonVoteds.Add(personVoted);
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
                var personVoted = db.PersonVoteds.SingleOrDefault(x => x.Id == id);
                db.PersonVoteds.Remove(personVoted);
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