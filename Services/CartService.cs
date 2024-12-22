using OrderMnagementAPIs.Repositories;
using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Services;


namespace OrderMnagementAPIs.Services
{
    public class CartService : ICartService
    {
        private readonly CartRepository _cartRepository;

        public CartService(CartRepository cartRepository)
        {
            _cartRepository = cartRepository;
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
