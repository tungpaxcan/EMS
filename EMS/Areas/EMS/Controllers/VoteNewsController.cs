using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Models;

namespace EMS.Areas.EMS.Controllers
{
    public class VoteNewsController : BaseController
    {
        private EMSEntities db = new EMSEntities();
        // GET: EMS/VoteNews
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult List()
        {
            try
            {
                var voteNews = (from a in db.VoteNews.Where(x => x.Id > 0)
                                select new
                                {
                                    id = a.Id,
                                    name = a.Name,
                                }).ToList();
                return Json(new { code = 200,voteNews = voteNews, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
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
                VoteNew voteNew = new VoteNew();
                voteNew.Name = name;
                voteNew.Status = false;
                db.VoteNews.Add(voteNew);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Edits(string status,int idVotenews)
        {
            try
            {
                var voteNews = db.VoteNews.SingleOrDefault(x => x.Id == idVotenews);
                if(status == "success-outlined")
                {
                    voteNews.Status = true;
                }
                else
                {
                    voteNews.Status = false;
                }
                db.SaveChanges();
                return Json(new { code = 200, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Deletes(int idVotenews)
        {
            try
            {
                var voteNews = db.VoteNews.SingleOrDefault(x => x.Id == idVotenews);
                var personVotedCount = db.PersonVoteds.Where(x => x.IdVoteNews == idVotenews);
                for (int i = 0; i < personVotedCount.Count(); i++)
                {
                    var idPersonVoted = personVotedCount.ToList().LastOrDefault().Id;
                    var personVoted = db.PersonVoteds.SingleOrDefault(x => x.Id == idPersonVoted);
                    db.PersonVoteds.Remove(personVoted);
                    db.SaveChanges();
                }
                db.VoteNews.Remove(voteNews);
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