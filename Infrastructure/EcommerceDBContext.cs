using System.Data;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class EcommerceDBContext
    {


            private readonly IConfiguration _configuration;
            public EcommerceDBContext(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public IDbConnection CreateConnection()
                => new SqlConnection(_configuration.GetConnectionString("SqlConnection"));
     
}
}
