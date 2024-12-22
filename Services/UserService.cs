using Microsoft.AspNetCore.Identity;
using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;
using OrderMnagementAPIs.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OrderMnagementAPIs.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUsersRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }


        public string Authenticate(string email, string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.UserEmail == email);
            if (user != null && user.UserPassword == HashPassword(password))
            {
                return GenerateJwtToken(user.UserId.ToString(), user.UserName);
            }
            return null;
        }

        public Users GetUser(string email, string password)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.UserEmail == email);
            if (user != null && user.UserPassword == HashPassword(password))
            {
                return user;
            }
            return null;
        }

        public UsersDTO GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            return user == null ? null : new UsersDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserPhone = user.UserPhone,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public void RegisterUser(Users user, string password)
        {
            user.UserPassword = HashPassword(password);
            _userRepository.Add(user);
        }

        public bool ValidateUser(string email, string password, out Users user)
        {
            user = _userRepository.GetAll().FirstOrDefault(u => u.UserEmail == email);
            return user != null && user.UserPassword == HashPassword(password);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private string GenerateJwtToken(string userId, string userName)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = jwtSettings["Key"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expireMinutes = jwtSettings["ExpireMinutes"];

            if (string.IsNullOrEmpty(key) || key.Length < 32)
            {
                throw new ArgumentException("The JWT Key must be at least 32 characters long.");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Name, userName)
    };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(expireMinutes)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }

}
