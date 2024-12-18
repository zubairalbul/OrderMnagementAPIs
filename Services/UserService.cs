using Microsoft.AspNetCore.Identity;
using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;
using OrderMnagementAPIs.Repositories;

namespace OrderMnagementAPIs.Services
{
    public class UsersService : IUsersService
    {
        private readonly UsersRepository _userRepository; // This field holds the repository for user-related database operations.
        private readonly IPasswordHasher<Users> _passwordHasher; // This field provides a mechanism for hashing and verifying user passwords.

        public UsersService(UsersRepository userRepository, IPasswordHasher<Users> passwordHasher)
        {
            _userRepository = userRepository; // Initialize the user repository dependency.
            _passwordHasher = passwordHasher; // Initialize the password hasher dependency.
        }

        // Registers a new user and hashes the password before saving.
        public void RegisterUser(Users user, string password)
        {
            user.Password = _passwordHasher.HashPassword(user, password); // Hash the user's password.
            _userRepository.Add(user); // Save the user to the repository.
        }

        // Validates a user's credentials by checking email and password.
        public bool ValidateUser(string email, string password, out Users user)
        {
            user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email); // Retrieve user by email.
            if (user != null && _passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success)
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
                UserId = user.Id, // Map user ID.
                UserName = user.Name, // Map user name.
                UserEmail = user.Email, // Map user email.
                UserPhone = user.Phone, // Map user phone.
                Role = user.Role, // Map user role.
                CreatedAt = user.CreatedAt // Map creation date.
            };
        }
    }
}
