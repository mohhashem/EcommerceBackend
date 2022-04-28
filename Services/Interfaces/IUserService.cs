
using Domain.Models;

namespace Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetAllUsers();
        public Task<UserDTO> GenerateUser(string fullname,  string password,string email);
        public Task<bool> Login(string email, string password);
    }
}
