using Domain.Models;
using Infrastructure.Interfaces;
using Newtonsoft.Json;
using Services.Interfaces;


namespace Services.Classes
{
    public class OrdersServices:IOrdersServices
    {
        private readonly IOrdersRepo _ordersRepo ;

        public OrdersServices (IOrdersRepo repository)
        {
            _ordersRepo = repository;
        }
        public async Task<IEnumerable<ProductDTO>> GetCartItems(string cartItems)
        {
            return JsonConvert.DeserializeObject<List<ProductDTO>>(cartItems);

        }
        public async Task<IEnumerable<CartProductDTO>> GetCartInfo(string products)
        {
            List<CartProductDTO> Cart = new List<CartProductDTO>();
            IEnumerable<ProductDTO> list = await GetCartItems(products);
            foreach(var i in list)
            {
                var productInfo = new CartProductDTO
                {
                    amount = i.amount,
                    productSID = i.productSID,
                    
                };
                Cart.Add(productInfo);
            }
            return Cart;
        }

        public async Task<IEnumerable<CartProductDTO>> getProducts(string products,int id)
        {

            IEnumerable<CartProductDTO> list = await GetCartInfo(products);
            return await _ordersRepo.GetCartProducts(list,id);

        }

       


        public async Task<int> CreateOrder( string address, string city, string building, int userid,int totalprice)
        {
           return await _ordersRepo.CreateOrder( address, city, building, userid,totalprice);
        }

        public  async Task<int> GetOrderId(int id)
        {
           return await _ordersRepo.getOrderId(id);
        }
    }
}
