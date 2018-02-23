using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Win.UI.Models
{
    public class UserProfileModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
    }
}