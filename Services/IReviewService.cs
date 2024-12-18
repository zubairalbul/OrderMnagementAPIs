using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Services
{
    public interface IReviewService
    {
        void AddReview(int userId, Review review);
        void DeleteReview(int reviewId, int userId);
        IEnumerable<ReviewDTO> GetReviewsByProduct(int productId, int page, int pageSize);
        void UpdateProductRating(int productId);
        void UpdateReview(Review review);
    }
}