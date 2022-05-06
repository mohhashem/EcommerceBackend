using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductDTO
    {
        public int amount { get; set; }
        public string productDesc { get; set; }
        public string productImageUrl { get; set; }
        public string productName { get; set; }
        public int productPrice { get; set; }
        public int productSID { get; set; }

    }
}
