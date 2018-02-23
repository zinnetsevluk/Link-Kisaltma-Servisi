using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkKS.DTO
{
    public class DTO_LINK
    {
        public Guid Id { get; set; }
        public string shortLink { get; set; }
        public string longLink { get; set; }
        public string password { get; set; }
        public DateTime? expiredDate { get; set; }
        public bool oneShot { get; set; }
        public bool notification { get; set; }
        //tıklanma süresi ????
        public DateTime createDateLink { get; set; }
        public Status STATUS { get; set; }
        public int ClickCount { get; set; }
    }
    public enum Status
    {
        Active = 0,
        Deleted = 1,
    }

}
