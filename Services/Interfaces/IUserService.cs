
using Domain.Models;

namespace Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDTO>> GetAllUsers();
        public Task<UserDTO> GenerateUser(string fullname,  string password,string email);
        public Task<string> Login(string email, string password);
        public Task<UserIdDTO> GetUserById(string email);
    }
}
