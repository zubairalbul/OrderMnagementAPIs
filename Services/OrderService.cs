using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;
using OrderMnagementAPIs.Repositories;

namespace OrderMnagementAPIs.Services
{
    // Order Service
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository; // Repository for managing orders.
        private readonly IProductsRepository _productRepository; // Repository for managing products.
        private readonly IOrderProductRepository _orderProductRepository; // Handles many-to-many relationship between orders and products.

        public OrderService(IOrderRepository orderRepository, IProductsRepository productRepository, IOrderProductRepository orderProductRepository)
        {
            _orderRepository = orderRepository; // Initialize order repository.
            _productRepository = productRepository; // Initialize product repository.
            _orderProductRepository = orderProductRepository; // Initialize order-product repository.
        }

        // Places a new order, calculates total cost, and reduces product stock.
        public void PlaceOrder(int userId, List<OrderProductDTO> items)
        {
            decimal totalAmount = 0; // Initialize total amount.
            foreach (var item in items)
            {
                var product = _productRepository.GetById(item.ProductId); // Fetch the product by ID.
                if (product == null || product.Stock < item.Quantity)
                    throw new InvalidOperationException($"Insufficient stock for product ID {item.ProductId}"); // Validate stock.
                totalAmount += product.Price * item.Quantity; // Calculate total cost.
                product.Stock -= item.Quantity; // Reduce stock quantity.
                _productRepository.Update(product); // Update product stock.
            }

            var order = new Order { UserId = userId, OrderDate = DateTime.Now, TotalAmount = totalAmount }; // Create new order.
            _orderRepository.Add(order); // Save order to repository.
            foreach (var item in items)
            {
                _orderProductRepository.Add(new OrderProduct { OrderId = order.Id, ProductId = item.ProductId, Quantity = item.Quantity }); // Save order-product relationship.
            }
        }

        // Retrieves all orders for a specific user.
        public IEnumerable<OrderDTO> GetOrdersByUser(int userId)
        {
            return _orderRepository.GetAll().Where(o => o.UserId == userId).Select(o => new OrderDTO
            {
                OrderId = o.Id, // Map order ID.
                UserId = o.UserId, // Map user ID.
                OrderDate = o.OrderDate, // Map order date.
                TotalAmount = o.TotalAmount // Map total amount.
            }).ToList();
        }

        // Retrieves order details by ID.
        public OrderDTO GetOrderById(int orderId)
        {
            var order = _orderRepository.GetById(orderId); // Fetch the order by ID.
            return order == null ? null : new OrderDTO
            {
                OrderId = order.Id, // Map order ID.
                UserId = order.UserId, // Map user ID.
                OrderDate = order.OrderDate, // Map order date.
                TotalAmount = order.TotalAmount // Map total amount.
            };
        }
    }


}
