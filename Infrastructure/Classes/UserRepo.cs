using Domain.Models;
using Infrastructure.Interfaces;
using Dapper;


namespace Infrastructure
{
    public class UserRepo : IUserRepo
    {
       private readonly EcommerceDBContext _context;
       public UserRepo(EcommerceDBContext context)
        {
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


            using (var connection = _context.CreateConnection())
            {
                var student = await connection.QueryAsync<UserDTO>(sqlQuery, new { fullname,password, email });
            }

            return new UserDTO
            {
                UserFullName = fullname,
                UserEmail = email,
                UserPassword = password,
              
            };
        }

        public async Task<bool> Login(string email, string password)
        {
            bool UserFound= false;
            var sqlQuery = "SELECT UserEmail,UserPassword FROM Users " +
                            "WHERE UserEmail= @email AND UserPassword = @password";


            using (var connection = _context.CreateConnection())
            {
                 var user = await connection.QueryAsync<UserDTO>(sqlQuery, new { email,password });
                if (user.Count() > 0) { UserFound = true; }
            }
            
            return UserFound;
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
    }
}