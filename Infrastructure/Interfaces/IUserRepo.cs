
using Domain.Models;


namespace Infrastructure.Interfaces
{
    public interface IUserRepo
    {
        public Task<IEnumerable<UserDTO>> GetAllUsers();
        public Task<bool> GenerateUser(string fullname, string password, string email);
        public Task<string> Login(string email, string password);
        public Task<bool> Exists(string email);
        public Task<UserIdDTO> GetUserById(string email);

    }
}
