using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Services
{
    public interface IUserService
    {
        string Authenticate(string email, string password);
        Users GetUser(string email, string password);
        UsersDTO GetUserById(int id);
        void RegisterUser(Users user, string password);
        bool ValidateUser(string email, string password, out Users user);
    }
}