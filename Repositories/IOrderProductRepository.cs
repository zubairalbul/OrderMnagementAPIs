using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Repositories
{
    public interface IOrderProductRepository
    {
        void Add(OrderProduct orderProduct);
        void Delete(OrderProduct orderProduct);
        IEnumerable<OrderProduct> GetAll();
        OrderProduct GetById(int orderId, int productId);
        void Update(OrderProduct orderProduct);
    }
}