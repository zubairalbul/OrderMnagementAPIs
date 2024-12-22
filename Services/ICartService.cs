using OrderMnagementAPIs.DTOs;
namespace OrderMnagementAPIs.Services


{
    public interface ICartService
    {
        void AddItemToCart(int userId, int productId, int quantity);
        void ClearCart(int userId);
        CartDTO GetCart(int userId);
        void RemoveItemFromCart(int userId, int productId);
    }
}