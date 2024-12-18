using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Repositories
{
    public interface IReviewRepository
    {
        void Add(Review review);
        void Delete(Review review);
        IEnumerable<Review> GetAll();
        Review GetById(int id);
        void Update(Review review);
    }
}