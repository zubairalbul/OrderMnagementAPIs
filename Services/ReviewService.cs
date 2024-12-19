using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;
using OrderMnagementAPIs.Repositories;

namespace OrderMnagementAPIs.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository; // This field holds the repository for managing reviews.
        private readonly IOrderRepository _orderRepository; // This field holds the repository for validating previous orders.
        private readonly IProductsRepository _productRepository; // This field holds the repository for managing product ratings.

        public ReviewService(IReviewRepository reviewRepository, IOrderRepository orderRepository, IProductsRepository productRepository)
        {
            _reviewRepository = reviewRepository; // Initialize the review repository dependency.
            _orderRepository = orderRepository; // Initialize the order repository dependency.
            _productRepository = productRepository; // Initialize the product repository dependency.
        }

        // Allows users to add a review for products they have purchased, ensuring duplicate reviews are not allowed.
        public void AddReview(int userId, Review review)
        {
            var order = _orderRepository.GetAll().FirstOrDefault(o => o.UserId == userId && o.OrderProducts.Any(op => op.ProductId == review.ProductId)); // Validate purchase.
            if (order == null)
                throw new InvalidOperationException("User has not purchased this product.");
            if (_reviewRepository.GetAll().Any(r => r.UserId == userId && r.ProductId == review.ProductId))
                throw new InvalidOperationException("Review already exists for this product.");
            review.ReviewDate = DateTime.Now; // Set the review date.
            _reviewRepository.Add(review); // Save the review to the repository.
            UpdateProductRating(review.ProductId); // Update the product's overall rating.
        }

        // Recalculates and updates the product's overall rating based on existing reviews.
        public void UpdateProductRating(int productId)
        {
            var reviews = _reviewRepository.GetAll().Where(r => r.ProductId == productId); // Fetch all reviews for the product.
            var product = _productRepository.GetById(productId); // Fetch the product by ID.
            product.OverallRating = (decimal)reviews.Average(r => r.Rating); // Calculate the average rating.
            _productRepository.Update(product); // Update the product's overall rating.
        }

        // Retrieves a paginated list of reviews for a specific product, mapping them to DTOs.
        public IEnumerable<ReviewDTO> GetReviewsByProduct(int productId, int page, int pageSize)
        {
            return _reviewRepository.GetAll().Where(r => r.ProductId == productId)
                .Skip((page - 1) * pageSize).Take(pageSize) // Apply pagination.
                .Select(r => new ReviewDTO
                {
                    ReviewId = r.ReviewId, // Map review ID.
                    UserId = r.UserId, // Map user ID.
                    ProductId = r.ProductId, // Map product ID.
                    Rating = r.Rating, // Map review rating.
                    Comment = r.Comment, // Map review comment.
                    ReviewDate = r.ReviewDate // Map review date.
                }).ToList();
        }

        // Updates an existing review. Ensures only the original user can modify the review.
        public void UpdateReview(Review review)
        {
            var existingReview = _reviewRepository.GetAll().FirstOrDefault(r => r.ReviewId == review.ReviewId && r.UserId == review.UserId); // Validate ownership.
            if (existingReview == null)
                throw new InvalidOperationException("Review not found or user not authorized.");
            existingReview.Rating = review.Rating; // Update rating.
            existingReview.Comment = review.Comment; // Update comment.
            _reviewRepository.Update(existingReview); // Save changes.
            UpdateProductRating(existingReview.ProductId); // Update the product's overall rating.
        }

        // Deletes a review. Ensures only the original user can delete the review.
        public void DeleteReview(int reviewId, int userId)
        {
            var review = _reviewRepository.GetAll().FirstOrDefault(r => r.ReviewId == reviewId && r.UserId == userId); // Validate ownership.
            if (review == null)
                throw new InvalidOperationException("Review not found or user not authorized.");
            _reviewRepository.Delete(review); // Remove the review.
            UpdateProductRating(review.ProductId); // Update the product's overall rating.
        }
    }
}
