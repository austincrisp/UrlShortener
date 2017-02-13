using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrlShortener.Models
{
    public class Click
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int BookmarkId { get; set; }

        public virtual Bookmark Bookmark { get; set; }
    }
}