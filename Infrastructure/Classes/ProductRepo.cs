using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Classes
{
    public class ProductRepo:IProductRep
    {

        private readonly EcommerceDBContext _context;
        public ProductRepo(EcommerceDBContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            string sqlQuery = "SELECT * FROM Products";
            using (var connection = _context.CreateConnection())
            {
                var products = await connection.QueryAsync<Products>(sqlQuery);
                return products.ToList();
            }
        }
    }
}

//https://acvshrstosystemshare.blob.core.windows.net/bootcamp22/e-commerce/LoremIpsum.txt
