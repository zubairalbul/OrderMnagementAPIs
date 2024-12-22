using Microsoft.AspNetCore.Mvc;
using OrderMnagementAPIs.Services;

namespace OrderMnagementAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetCart(int userId)
        {
            var cart = _cartService.GetCart(userId);
            return Ok(cart);
        }

        [HttpPost("{userId}/add")]
        public IActionResult AddItemToCart(int userId, [FromQuery] int productId, [FromQuery] int quantity)
        {
            _cartService.AddItemToCart(userId, productId, quantity);
            return Ok("Item added to cart.");
        }

        [HttpDelete("{userId}/remove")]
        public IActionResult RemoveItemFromCart(int userId, [FromQuery] int productId)
        {
            _cartService.RemoveItemFromCart(userId, productId);
            return Ok("Item removed from cart.");
        }

        [HttpDelete("{userId}/clear")]
        public IActionResult ClearCart(int userId)
        {
            _cartService.ClearCart(userId);
            return Ok("Cart cleared.");
        }
    }

}
