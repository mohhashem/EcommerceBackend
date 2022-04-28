using Domain;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserRepo
    {
        public Task<IEnumerable<UserDTO>> GetAllUsers();
        public Task<UserDTO> GenerateUser(string fullname, string password, string email);
        public Task<bool> Login(string email, string password);
        public Task<bool> Exists(string email);

    }
}
