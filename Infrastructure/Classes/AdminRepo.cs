using Dapper;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharpHash.Base;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Classes
{

    public class AdminRepo:IAdminRepo
    {
        private readonly EcommerceDBContext _context;
        private readonly IConfiguration _configuration;
        public AdminRepo(EcommerceDBContext context,IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;

        }

        public async Task<Admin> GenerateAdmin( string name, string password)
        {

            var sqlQuery = "INSERT INTO Admins(AdminName,AdminPassword) " +
                            "VALUES( @name,@password)";

            password = HashFactory.Crypto.CreateMD5()
                    .ComputeString(password, Encoding.UTF8).ToString();

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<Admin>(sqlQuery, new { password, name});
            }

            return new Admin
            {
                AdminName = name,
                AdminPassword = password,

            };
        }

        public async Task<string> Login(string email, string password)
        {
            string token = " ";
            password = HashFactory.Crypto.CreateMD5()
                     .ComputeString(password, Encoding.UTF8).ToString();
            var sqlQuery = "SELECT AdminName,AdminPassword FROM Admins " +
                            "WHERE AdminName= @email AND AdminPassword = @password";


            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<UserDTO>(sqlQuery, new { email, password });
                if (user.Count() > 0) { token = CreateToken(email,password); }
            }

            return token;
        }

        private string CreateToken(string name,string password)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims:claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
