using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UrlShortener.Models
{
    public class Like
    {
        public int Id { get; set; }

        public string RequestorId { get; set; }

        [ForeignKey("RequestorId")]
        public virtual ApplicationUser Requestor { get; set; }

        public int TargetId { get; set; }

        [ForeignKey("TargetId")]
        public virtual Bookmark Target { get; set; }
    }
}