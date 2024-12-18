using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Repositories
{
    public interface IUsersRepository
    {
        void Add(Users user);
        void Delete(Users user);
        IEnumerable<Users> GetAll();
        Users GetById(int id);
        void Update(Users user);
    }
}