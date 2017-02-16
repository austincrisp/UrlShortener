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
            var likedBookmarks = db.Likes.Where(l => l.RequestorId == userInstance.Id).Include("Target");
            ViewBag.likedBookmarks = likedBookmarks;
            return View(userInstance);
        }

        [HttpPost]
        [Route("u/{username}")]
        public ActionResult AddLike(string username)
        {
            var me = User.Identity.GetUserId();
            var target = int.Parse(Request.Form["BookmarkId"]);
            Like favorite = new Like
            {
                RequestorId = me,
                TargetId = target
            };
            db.Likes.Add(favorite);
            db.SaveChanges();
            return RedirectToAction("Detail");
        }
    }
}