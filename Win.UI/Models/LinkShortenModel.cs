using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web;

namespace Win.UI.Models
{
    public class LinkShortenModel
    {
        public string LongLink { get; set; }
        public Guid? UserId { get; set; }
        public string Password { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool Notification { get; set; }
        public bool OneShot { get; set; }
        public byte status { get; set; }
       
    }

}
