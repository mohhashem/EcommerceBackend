using Domain.Models;
using Infrastructure.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Classes
{
    public class ProductService:IProductService
    {
        private readonly IProductRep _productRep;

        public ProductService(IProductRep repository)
        {
            _productRep = repository;
        }

        public Task<Product> Addproduct(string ProductName, string ProductDesc, int ProductPrice, string ProductImageUrl)
        {
            return _productRep.Addproduct(ProductName, ProductDesc, ProductPrice, ProductImageUrl);
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            return _productRep.GetProducts();
        }
    }
}
