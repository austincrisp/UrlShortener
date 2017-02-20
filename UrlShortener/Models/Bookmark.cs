using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UrlShortener.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }

        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public bool doesOwnerLike(ApplicationUser Owner)
        {
            if ()
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}