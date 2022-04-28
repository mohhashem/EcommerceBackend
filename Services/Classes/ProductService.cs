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

        public Task<IEnumerable<Products>> GetProducts()
        {
            return _productRep.GetProducts();
        }
    }
}
