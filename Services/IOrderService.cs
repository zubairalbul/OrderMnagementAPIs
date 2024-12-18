using OrderMnagementAPIs.DTOs;

namespace OrderMnagementAPIs.Services
{
    public interface IOrderService
    {
        OrderDTO GetOrderById(int orderId);
        IEnumerable<OrderDTO> GetOrdersByUser(int userId);
        void PlaceOrder(int userId, List<OrderProductDTO> items);
    }
}