using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Services
{
    public interface IUsersService
    {
        UsersDTO GetUserById(int id);
        void RegisterUser(Users user, string password);
        bool ValidateUser(string email, string password, out Users user);
    }
}