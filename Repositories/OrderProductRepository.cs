using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Repositories
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly OrderProductRepository _orderProductRepository;
        public OrderProductRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<OrderProduct> GetAll() => _context.Set<OrderProduct>().ToList();
        public OrderProduct GetById(int orderId, int productId) => _context.Set<OrderProduct>().Find(orderId, productId);
        public void Add(OrderProduct orderProduct)
        {
            _context.Set<OrderProduct>().Add(orderProduct);
            _context.SaveChanges();
        }


        public void Update(OrderProduct orderProduct)
        {
            _context.Set<OrderProduct>().Update(orderProduct);
            _context.SaveChanges();
        }
        public void Delete(OrderProduct orderProduct)
        {
            _context.Set<OrderProduct>().Remove(orderProduct);
            _context.SaveChanges();
        }
    }
}
