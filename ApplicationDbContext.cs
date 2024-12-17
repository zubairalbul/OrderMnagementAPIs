using Microsoft.EntityFrameworkCore;

namespace OrderMnagementAPIs
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }

    }
}
