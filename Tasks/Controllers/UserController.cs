using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Tasks.Controllers
{



    [ApiController]
    [Route("[controller]")]

    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService todoService)
        {
            _userService = todoService;

        }


        [HttpPost]
        [Route("GenerateUser")]
        public async Task<IActionResult> GenerateUser(UserDTO users)
        {
            try
            {
                var user = await _userService.GenerateUser(users.UserFullName, users.UserPassword,users.UserEmail);
                return Ok(user);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("getId")]
        public async Task<IActionResult> GetUserById(string email)
        {
            try
            {
                var id = await _userService.GetUserById( email);
                return Ok(id);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("UserLogin")]
        public async Task<IActionResult> Login(string email,string password)
        {
            try
            {
                var user = await _userService.Login(email,password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}
