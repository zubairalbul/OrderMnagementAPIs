using Microsoft.AspNetCore.Identity;
using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;
using OrderMnagementAPIs.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace OrderMnagementAPIs.Services
{
    // User Service: This service handles all user-related operations, such as registration, validation, and retrieving user details.
    public class UserService : IUserService
    {
        private readonly IUsersRepository _userRepository; // This field holds the repository for user-related database operations.

        public UserService(IUsersRepository userRepository)
        {
            _userRepository = userRepository; // Initialize the user repository dependency.
        }

        // Hashes the password manually using SHA256.
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password); // Convert the password to bytes.
                var hash = sha256.ComputeHash(bytes); // Compute the hash.
                return Convert.ToBase64String(hash); // Convert the hash to a Base64 string.
            }
        }

        // Registers a new user and hashes the password before saving.
        public void RegisterUser(Users user, string password)
        {
            user.UserPassword = HashPassword(password); // Hash the user's password manually.
            _userRepository.Add(user); // Save the user to the repository.
        }

        // Validates a user's credentials by checking email and password.
        public bool ValidateUser(string email, string password, out Users user)
        {
            user = _userRepository.GetAll().FirstOrDefault(u => u.UserEmail == email); // Retrieve user by email.
            if (user != null && user.UserPassword == HashPassword(password))
            {
                return true; // Return true if the password matches.
            }
            return false; // Return false if validation fails.
        }

        // Retrieves user details by ID and maps them to a UserDTO.
        public UsersDTO GetUserById(int id)
        {
            var user = _userRepository.GetById(id); // Fetch the user by ID.
            return user == null ? null : new UsersDTO
            {
                UserId = user.UserId, // Map user ID.
                UserName = user.UserName, // Map user name.
                UserEmail = user.UserEmail, // Map user email.
                UserPhone = user.UserPhone, // Map user phone.
                Role = user.Role, // Map user role.
                CreatedAt = user.CreatedAt // Map creation date.
            };
        }
    }

}
