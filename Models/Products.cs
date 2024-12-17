using System.ComponentModel.DataAnnotations;

namespace OrderMnagementAPIs.Models
{
    public class Products
    {
        [Key] // Marks this property as the primary key in the database
        public int ProductId { get; set; } // Primary Key

        [Required] // Ensures this property must have a value
        public string ProductName { get; set; } // Product name, required field

        public string Description { get; set; } // Optional description of the product

        [Required] // Ensures this property must have a value
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")] // Validates price is positive
        public decimal Price { get; set; } // Product price, e.g., 19.99

        [Required] // Ensures this property must have a value
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")] // Validates stock is non-negative
        public int Stock { get; set; } // Number of items in stock, e.g., 100

        public decimal OverallRating { get; set; } // Average rating calculated from reviews

        // Relationships
       // public ICollection<Review> Reviews { get; set; } = new List<Review>(); // A product can have multiple reviews
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>(); // A product can belong to multiple orders
    }
}
