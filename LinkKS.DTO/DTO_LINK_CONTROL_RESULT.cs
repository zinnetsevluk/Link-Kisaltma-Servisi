using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkKS.DTO
{
    public class DTO_LINK_CONTROL_RESULT
    {
        public Guid linkId { get; set; }
        public string longLink { get; set; }
        public bool password { get; set; }
        public bool oneShot { get; set; }
       
    }
}
