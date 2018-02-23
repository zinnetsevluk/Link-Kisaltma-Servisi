using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Win.UI.Models
{
    public class LinkListModel
    {
        public Guid Id { get; set; }
        public string linkPassword { get; set; }
        public string shortLink { get; set; }
        public string longLink { get; set; }
        public DateTime? expireDate { get; set; }
        public bool oneShot { get; set; }
        public bool notification { get; set; }
        public DateTime CreateDate { get; set; }
        public bool status { get; set; }
        public int linkLongCount { get; set; }

    }
}