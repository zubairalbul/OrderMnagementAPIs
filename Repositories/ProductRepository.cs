using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductsRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<Products> GetAll() => _context.Set<Products>().ToList();
        public Products GetById(int id) => _context.Set<Products>().Find(id);
        public void Add(Products product)
        {
            _context.Set<Products>().Add(product);
            _context.SaveChanges();
        }
        public void Update(Products product)
        {
            _context.Set<Products>().Update(product);
            _context.SaveChanges();
        }
        public void Delete(Products product)
        {
            _context.Set<Products>().Remove(product);
            _context.SaveChanges();
        }
    }
}
