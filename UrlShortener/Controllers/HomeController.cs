using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string LongUrl)
        {
            return View(db.Bookmarks.ToList());
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

        [Route("u/{username}")]
        public ActionResult Detail(string userName)
        {
            ApplicationUser userInstance = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            string me = User.Identity.GetUserId();
            string target = userInstance.Id;
            bool isLiked = db.Likes
                .Where(
                    l => (l.RequestorId == me && l.TargetId == target) ||
                         (l.TargetId == me && l.RequestorId == target)
                ).Any();
            ViewBag.isLiked = isLiked;
            return View(userInstance);
        }

        [HttpPost]
        [Route("u/{username}")]
        public ActionResult AddLike(string userName)
        {
            var me = User.Identity.GetUserId();
            string target = db.Users.Where(u => u.UserName == userName).FirstOrDefault().Id;
            Like favorite = new Like
            {
                RequestorId = me,
                TargetId = target
            };
            db.Likes.Add(favorite);
            db.SaveChanges();
            return RedirectToAction("Profile");
        }
    }
}