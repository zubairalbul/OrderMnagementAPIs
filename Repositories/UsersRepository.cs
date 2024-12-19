using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public IEnumerable<Users> GetAll() => _context.Set<Users>().ToList();
        public Users GetById(int id) => _context.Set<Users>().Find(id);
        public void Add(Users user)
        {
            _context.Set<Users>().Add(user);
            _context.SaveChanges();
        }
        public void Update(Users user)
        {
            _context.Set<Users>().Update(user);
            _context.SaveChanges();
        }
        public void Delete(Users user)
        {
            _context.Set<Users>().Remove(user);
            _context.SaveChanges();
        }
    }
}
