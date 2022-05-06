using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IAdminRepo
    {
        public Task<Admin> GenerateAdmin( string email, string password);
        public Task<string> Login(string email, string password);
    }
}
