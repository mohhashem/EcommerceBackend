using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrdersServices _ordersservices;

        public OrdersController(IOrdersServices todoService)
        {
            _ordersservices = todoService;

        }


        [HttpGet]
        [Route("getOrderId")]
        public async Task<IActionResult> GetOrderId(int Id)
        {
            try
            {
                int id = await _ordersservices.GetOrderId(Id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder(Order orders)
        {
            try
            {
                var order = await _ordersservices.CreateOrder(orders.address,orders.city,orders.building,orders.UserSID,orders.totalprice);
                return Ok(order);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("GetCartItems")]
        public async Task<IActionResult> GetCartItems(string cart,int OrderSID)
        {
            try
            {
                var order = await _ordersservices.getProducts(cart,OrderSID);
                return Ok(order);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
