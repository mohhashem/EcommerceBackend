using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IProductRep
    {
        public Task<Product> Addproduct(string ProductName, string ProductDesc, int ProductPrice, string ProductImageUrl);
        public Task<IEnumerable<Product>> GetProducts();
    }
}
