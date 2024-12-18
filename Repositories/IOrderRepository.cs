using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Repositories
{
    public interface IOrderRepository
    {
        void Add(Order order);
        void Delete(Order order);
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        void Update(Order order);
    }
}