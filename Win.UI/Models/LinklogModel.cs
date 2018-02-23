using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Win.UI.Models
{
    public class LinklogModel
    {
        public Guid Id { get; set; }
        public string IP { get; set; }
        public string Agent { get; set; }
        public string Referrer { get; set; }
        public DateTime Date { get; set; }
    }
}