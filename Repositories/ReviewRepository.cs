using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Repositories
{
    public class ReviewRepository :IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<Review> GetAll() => _context.Set<Review>().ToList();
        public Review GetById(int id) => _context.Set<Review>().Find(id);
        public void Add(Review review)
        {
            _context.Set<Review>().Add(review);
            _context.SaveChanges();
        }
        public void Update(Review review)
        {
            _context.Set<Review>().Update(review);
            _context.SaveChanges();
        }
        public void Delete(Review review)
        {
            _context.Set<Review>().Remove(review);
            _context.SaveChanges();
        }
    }
}
