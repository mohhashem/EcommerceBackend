using Domain.Models;
using Infrastructure.Interfaces;
using Dapper;
using SharpHash.Base;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class UserRepo : IUserRepo
    {
       private readonly EcommerceDBContext _context;
        private readonly IConfiguration _configuration;
        public UserRepo(EcommerceDBContext context,IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;

        }
        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            string sqlQuery = "SELECT * FROM Users";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<UserDTO>(sqlQuery);
                return users.ToList();
            }
        }

        public async Task<UserDTO> GenerateUser(string fullname, string password,string email)
        {

            var sqlQuery = "INSERT INTO Users(UserFullName,UserEmail,UserPassword) " +
                            "VALUES( @fullname,@email,@password)";

            password   = HashFactory.Crypto.CreateMD5()
                    .ComputeString(password,Encoding.UTF8).ToString();

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<UserDTO>(sqlQuery, new { fullname,password, email });
            }

            return new UserDTO
            {
                UserFullName = fullname,
                UserEmail = email,
                UserPassword = password,
              
            };
        }

        public async Task<UserIdDTO> GetUserById(string email)
        {
            var sqlQuery = "Select * from Users where UserEmail=@email";
            using (var connection = _context.CreateConnection())
            {
               return await connection.QuerySingleAsync<UserIdDTO>(sqlQuery,new {email});
               
            }
        }

        public async Task<string> Login(string email, string password)
        {
            string token = " ";
            password = HashFactory.Crypto.CreateMD5()
                     .ComputeString(password, Encoding.UTF8).ToString();
            var sqlQuery = "SELECT UserEmail,UserPassword FROM Users " +
                            "WHERE UserEmail= @email AND UserPassword = @password";


            using (var connection = _context.CreateConnection())
            {
                 var user = await connection.QueryAsync<UserDTO>(sqlQuery, new { email,password });
                if (user.Count() > 0) { token = CreateToken(email,password); }
            }
            
            return token;
        }

        public async Task<bool> Exists(string email)
        {
            bool exists= false;
            var sqlQuery = "SELECT UserEmail" +
                            "FROM Users" +
                            "WHERE UserEmail = @email";

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QueryAsync<UserDTO>(sqlQuery, new { email });
                if (user.Count() > 0) { exists = true; }
            }
            return exists;
        }

        private string CreateToken(string name, string password)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}