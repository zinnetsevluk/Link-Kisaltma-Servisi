using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkKS.DTO
{
    public class DTO_LINK_LOG
    {
        public string IP { get; set; }
        public string Agent { get; set; }
        public string Referrer { get; set; }
        public DateTime Date { get; set; }
    }
}
