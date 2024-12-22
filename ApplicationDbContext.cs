using Microsoft.EntityFrameworkCore;
using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs
{
    public class ApplicationDbContext : DbContext
  {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; } // DbSet for Users table
        public DbSet<Products> Products { get; set; } // DbSet for Products table
        public DbSet<Order> Orders { get; set; } // DbSet for Orders table
        public DbSet<OrderProduct> OrderProducts { get; set; } // DbSet for OrderProducts table
        public DbSet<Review> Reviews { get; set; } // DbSet for Reviews table
        public DbSet<Cart> Carts { get; set; }  //Db set for Cart table
        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Calls the base class implementation to ensure proper initialization

            // Composite Key for OrderProduct
            modelBuilder.Entity<OrderProduct>() // Configures the OrderProduct entity
                .HasKey(op => new { op.OrderId, op.ProductId }); // Defines a composite key combining OrderId and ProductId
        }
    }
}



    

