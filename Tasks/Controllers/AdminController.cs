using Domain.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace UserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class AdminController : Controller
    {

        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices todoService)
        {
            _adminServices = todoService;

        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login(string email,string password)
        {
            try
            {
                var Admin = await _adminServices.Login(email,password);
                return Ok(Admin);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("GenerateAdmin")]
        public async Task<IActionResult> GenerateUser(Admin admin)
        {
            try
            {
                var Admin = await _adminServices.GenerateAdmin( admin.AdminName, admin.AdminPassword);
                return Ok(Admin);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
        
}
