using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Models;

namespace EMS.Controllers
{
    public class HomeController : BaseHomeController
    {
        private EMSEntities db = new EMSEntities();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public JsonResult Vote()
        {
            try
            {
                var personVoted = (from a in db.PersonVoteds.Where(x => x.VoteNew.Status == true)
                                   select new
                                   {
                                       name = a.Customer.Name,
                                       id = a.Id,
                                       idCustomer = a.Customer.Id,
                                       idVotesNew = a.VoteNew.Id,
                                   }).ToList();
                var voteNews = (from a in db.VoteNews.Where(x => x.Status == true)
                                   select new
                                   {
                                       idVotesNew = a.Id,
                                   }).ToList();
                return Json(new { code = 200, personVoted= personVoted,voteNews= voteNews, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult AddsVote(int idPersonVote)
        {
            try
            {
                var customer = (Customer)Session["user"];
                var id = customer.Id;
                Vote vote = new Vote();
                vote.IdPersonVoted = idPersonVote;
                vote.Vote1 = id;
                vote.Status = true;
                db.Votes.Add(vote);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Đăng nhập thất bại" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "Đăng nhập thất bại" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public JsonResult HideButton(int idVoteNew)
        {
            try
            {
                var customer = (Customer)Session["user"];
                var id = customer.Id;
                var checkVotes = db.Votes.SingleOrDefault(x => x.PersonVoted.VoteNew.Id == idVoteNew && x.Vote1 == id);
                if (checkVotes == null)
                {
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