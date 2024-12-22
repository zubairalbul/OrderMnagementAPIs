using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Repositories
{
    public interface ICartRepository
    {
        void AddItemToCart(int userId, int productId, int quantity);
        void ClearCart(int userId);
        Cart GetCartByUserId(int userId);
        void RemoveItemFromCart(int userId, int productId);
    }
}