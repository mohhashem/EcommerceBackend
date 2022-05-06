using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;

namespace Infrastructure.Classes
{
    public class OrdersRepo : IOrdersRepo
    {
        private readonly EcommerceDBContext _context;
        public OrdersRepo(EcommerceDBContext context)
        {
            _context = context;

        }

        public async Task<int> CreateOrder(string address, string city, string building, int userid, int totalprice)
        {

            var sqlQuery = "INSERT INTO Orders(address,city,building,UserSID,totalprice) " +
                            "VALUES(@address,@city,@building,@userid,@totalprice)" +
                            "select scope_identity()";


            using (var connection = _context.CreateConnection())
            {
                var order = await connection.QuerySingleAsync<int>(sqlQuery, new { address, city, building, userid, totalprice });
                return order;
            }
            

            
        }

        public async Task<IEnumerable<CartProductDTO>> GetCartProducts(IEnumerable<CartProductDTO> list, int OrderSID)
        {

            using (var connection = _context.CreateConnection())
            {


                foreach (var product in list)
                {
                    var sqlQuery = "INSERT INTO OrderProducts(OrderSID,productSID,amount) " +
                           "VALUES(@OrderSID,@productSID,@amount)";
                    var order = await connection.QueryAsync(sqlQuery, new { OrderSID, product.productSID, product.amount });

                }

            }
            return new List<CartProductDTO>
            {
                new CartProductDTO()
                {
                    amount = 0,
                    productSID =1
                }
            };


        }

        public async Task<int> getOrderId(int id)
        {
            var sqlQuery = "Select OrderSID from Orders where UserSID=@id";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleAsync<int>(sqlQuery, new { id });
                return result;

            }
        }
    }

}

