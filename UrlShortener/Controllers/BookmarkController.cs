using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UrlShortener.Models;
using Microsoft.AspNet.Identity;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace UrlShortener.Controllers
{
    [Authorize]
    public class BookmarkController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Blog
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var bookmarks = db.Bookmarks.Include(b => b.Owner).Where(b => b.OwnerId == userId);
            return View(bookmarks);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,LongUrl,OwnerId")] Bookmark bookmark)
        {
            if (ModelState.IsValid)
            {
                string output;
                byte[] byteData = Encoding.ASCII.GetBytes(bookmark.LongUrl);
                Stream inputStream = new MemoryStream(byteData);

                using (SHA256 shaM = new SHA256Managed())
                {
                    var result = shaM.ComputeHash(inputStream);
                    output = BitConverter.ToString(result);
                }
                bookmark.ShortUrl = output.Replace("-", "").Substring(0, 5);

                bookmark.OwnerId = User.Identity.GetUserId();
                db.Bookmarks.Add(bookmark);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookmark);
        }
    }
}