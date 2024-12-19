using Microsoft.AspNetCore.Mvc;
using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Services;

namespace OrderMnagementAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult PlaceOrder(int userId, List<OrderProductDTO> items)
        {
            _orderService.PlaceOrder(userId, items);
            return Ok("Order placed successfully.");
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetOrdersByUser(int userId)
        {
            var orders = _orderService.GetOrdersByUser(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound("Order not found.");
            return Ok(order);
        }
    }

}
