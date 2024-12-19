using Microsoft.AspNetCore.Mvc;
using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;
using OrderMnagementAPIs.Services;

namespace OrderMnagementAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public IActionResult AddReview(int userId, ReviewDTO reviewDto)
        {
            var review = new Review
            {
                UserId = userId,
                ProductId = reviewDto.ProductId,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment,
                ReviewDate = DateTime.Now
            };

            _reviewService.AddReview(userId, review);
            return Ok("Review added successfully.");
        }

        [HttpGet("product/{productId}")]
        public IActionResult GetReviewsByProduct(int productId, int page = 1, int pageSize = 10)
        {
            var reviews = _reviewService.GetReviewsByProduct(productId, page, pageSize);
            return Ok(reviews);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReview(int id, int userId, ReviewDTO reviewDto)
        {
            var review = new Review
            {
                ReviewId = id,
                UserId = userId,
                ProductId = reviewDto.ProductId,
                Rating = reviewDto.Rating,
                Comment = reviewDto.Comment
            };

            _reviewService.UpdateReview(review);
            return Ok("Review updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id, int userId)
        {
            _reviewService.DeleteReview(id, userId);
            return Ok("Review deleted successfully.");
        }
    }

}
