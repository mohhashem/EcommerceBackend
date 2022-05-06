using Domain.Models;
using Infrastructure.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Classes
{
    public class AdminServices:IAdminServices
    {
        private readonly IAdminRepo _adminRepo;
        public AdminServices(IAdminRepo repo)
        {
            _adminRepo = repo;

        }

        public async Task<string> Login(string email, string password)
        {
            return await _adminRepo.Login(email, password);
        }

        public async Task<Admin> GenerateAdmin( string email, string password)
        {
            return await _adminRepo.GenerateAdmin( email, password);
        }
    }
}
