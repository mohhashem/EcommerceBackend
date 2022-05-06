using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {
     
        public string address { get; set; }
        public string city { get; set; }
        public string building { get; set; }
        public int UserSID { get; set; }
        public int totalprice { get; set; }
    }
}
