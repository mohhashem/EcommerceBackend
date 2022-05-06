using Azure.Storage.Blobs;
using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;
using Newtonsoft.Json;
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

        public async Task<Product> Addproduct(string ProductName, string ProductDesc,int ProductPrice, string ProductImageUrl)
        {

            var sqlQuery = "INSERT INTO Products(ProductName,ProductDesc,ProductPrice,ProductImageUrl) " +
                            "VALUES( @ProductName,@ProductDesc,@ProductPrice,@ProductImageUrl)";


            using (var connection = _context.CreateConnection())
            {
                var product = await connection.QueryAsync<UserDTO>(sqlQuery, new { ProductName, ProductDesc, ProductPrice,ProductImageUrl});
            }

            return new Product
            {
                ProductName = ProductName,
                ProductDesc = ProductDesc,
                ProductPrice = ProductPrice,
                ProductImageUrl = ProductImageUrl,

            };
        }



        public async Task<IEnumerable<Product>> GetProducts()
        {
            string sqlQuery = "SELECT * FROM Products";
            using (var connection = _context.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>(sqlQuery);
                return products.ToList();
            }
        }

        public async static void addingImages()
        {
            BlobServiceClient bSC = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=acvshrstosystemshare;AccountKey=lbEEm7shRJFF7GXwiWS4qGYICZrQ+S9WMq1gcqNeXITWEiOkL1PkLL5kbgrIQqi7Ux2PV8trKXTzj1LWbvV6zQ==;EndpointSuffix=core.windows.net");
            string filename = $"../Images/Beyrouth.jpg";
            string localFilePath = Path.Combine(filename);
            var containerClient = bSC.GetBlobContainerClient($"bootcamp22");
            string uploadPath = "/city-reviewing/" + filename.Replace("../", string.Empty);
            BlobClient blobClient = containerClient.GetBlobClient(uploadPath);
            using FileStream uploadFileStream = File.OpenRead(localFilePath);
            var result = await blobClient.UploadAsync(uploadFileStream, true);
            uploadFileStream.Close();
        }
    }
}

//https://acvshrstosystemshare.blob.core.windows.net/bootcamp22/e-commerce/LoremIpsum.txt
