using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<Order> GetAll() => _context.Set<Order>().ToList();
        public Order GetById(int id) => _context.Set<Order>().Find(id);
        public void Add(Order order)
        {
            _context.Set<Order>().Add(order);
            _context.SaveChanges();
        }
        

        public void Update(Order order)
        {
            _context.Set<Order>().Update(order);
            _context.SaveChanges();
        }
        public void Delete(Order order)
        {
            _context.Set<Order>().Remove(order);
            _context.SaveChanges();
        }
    }
}
