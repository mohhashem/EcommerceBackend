using Domain.Models;
using Infrastructure.Interfaces;

namespace Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepo _userRepo;
        
        public UserService(IUserRepo repository)
        {
            _userRepo = repository;
        }

        public Task<UserDTO> GenerateUser(string fullname, string password, string email)
        {

            return _userRepo.GenerateUser(fullname, password, email);
        }

        public Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }

        public Task<UserIdDTO> GetUserById(string email)
        {
            return _userRepo.GetUserById(email);
        }

        public Task<string> Login(string email, string password)
        {
            return _userRepo.Login(email, password);
        }

       
    }
}