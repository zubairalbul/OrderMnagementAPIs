using Microsoft.EntityFrameworkCore;
using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Cart GetCartByUserId(int userId)
        {
            return _context.Carts.Include(c => c.Items).FirstOrDefault(c => c.UserId == userId);
        }


        public void AddItemToCart(int userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId) ?? CreateCart(userId);
            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }

            _context.SaveChanges();
        }

        public void RemoveItemFromCart(int userId, int productId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
                if (cartItem != null)
                {
                    cart.Items.Remove(cartItem);
                    _context.SaveChanges();
                }
            }
        }

        public void ClearCart(int userId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.Items);
                _context.SaveChanges();
            }
        }

        private Cart CreateCart(int userId)
        {
            var cart = new Cart { UserId = userId };
            _context.Carts.Add(cart);
            _context.SaveChanges();
            return cart;
        }
    }

}
