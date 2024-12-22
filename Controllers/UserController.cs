using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;
using OrderMnagementAPIs.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrderMnagementAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(UsersDTO userDto, string password)
        {
            _userService.RegisterUser(new Users
            {
                UserName = userDto.UserName,
                UserEmail = userDto.UserEmail,
                UserPhone = userDto.UserPhone,
                Role = userDto.Role,
                CreatedAt = DateTime.Now
            }, password);

            return Ok("User registered successfully.");
        }
       
        [NonAction]
        public string GenerateJwtToken(string userId, string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, userId),
        new Claim(JwtRegisteredClaimNames.UniqueName, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) };
    

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: claims,expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryInMinutes"])),signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Authenticates a user and returns a JWT token
        // Authenticates a user and returns a JWT token
        [AllowAnonymous]
        // Action method for handling GET requests to log in a user.
        [HttpGet("Login")]
        public IActionResult Login(string email, string password)
        {
            var user = _userService.GetUser(email, password);
            if (user != null)
            {
                string token = _userService.Authenticate(user.UserEmail, password);
                return Ok(new { Token = token, User = user });
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }



        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }
    }
}
