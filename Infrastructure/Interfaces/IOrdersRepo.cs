using Domain.Models;


namespace Infrastructure.Interfaces
{
    public interface IOrdersRepo
    {
        public Task<IEnumerable<CartProductDTO>> GetCartProducts(IEnumerable<CartProductDTO> list,int id);
        public Task<int> CreateOrder( string address, string city, string building, int userid,int totalprice);
        public Task<int> getOrderId(int id);
    }
}
