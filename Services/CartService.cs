using OrderMnagementAPIs.Repositories;
using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Services;
using OrderMnagementAPIs.Models;


namespace OrderMnagementAPIs.Services
{
    public class CartService : ICartService
    {
        private readonly CartRepository _cartRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        public CartService(CartRepository cartRepository, IProductsRepository productsRepository, IOrderRepository orderRepository, IOrderProductRepository orderProductRepository)
        {
            _cartRepository = cartRepository;
            _productsRepository = productsRepository;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
        }

        public CartDTO GetCart(int userId)
        {
            var cart = _cartRepository.GetCartByUserId(userId);
            if (cart == null) return new CartDTO { Items = new List<CartItemDTO>() };

            return new CartDTO
            {
                UserId = userId,
                Items = cart.Items.Select(i => new CartItemDTO
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
        public void Checkout(int userId)
        {
            var cart = _cartRepository.GetCartByUserId(userId);
            if (cart == null || !cart.Items.Any())
            {
                throw new InvalidOperationException("Cart is empty. Add items before checkout.");
            }

            decimal totalAmount = 0;
            foreach (var item in cart.Items)
            {
                var product = _productsRepository.GetById(item.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Product with ID {item.ProductId} does not exist.");
                }
                if (product.Stock < item.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient stock for product '{product.ProductName}'.");
                }

                // Calculate the total amount
                totalAmount += product.Price * item.Quantity;

                // Reduce stock
                product.Stock -= item.Quantity;
                _productsRepository.Update(product);
            }

            // Create an order
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount
            };
            _orderRepository.Add(order);

            // Transfer cart items to the order
            foreach (var item in cart.Items)
            {
                _orderProductRepository.Add(new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }

            // Clear the cart
            _cartRepository.ClearCart(userId);
        }

        public void AddItemToCart(int userId, int productId, int quantity)
        {
            _cartRepository.AddItemToCart(userId, productId, quantity);
        }

        public void RemoveItemFromCart(int userId, int productId)
        {
            _cartRepository.RemoveItemFromCart(userId, productId);
        }

        public void ClearCart(int userId)
        {
            _cartRepository.ClearCart(userId);
        }
    }

}
