using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Win.UI.Models
{
    public class CheckPasswordModel
    {
        public Guid LinkId { get; set; }
        public string Password { get; set; }
    }
}