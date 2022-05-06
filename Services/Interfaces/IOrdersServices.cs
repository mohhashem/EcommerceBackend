using Domain.Models;


namespace Services.Interfaces
{
    public interface IOrdersServices
    {
        
        public Task<IEnumerable<CartProductDTO>> getProducts(string products,int id);
        public Task<int> CreateOrder( string address, string city, string building, int userid, int totalprice);
       public Task<int> GetOrderId(int id);
    }
}
