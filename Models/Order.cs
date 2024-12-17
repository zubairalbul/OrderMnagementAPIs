using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderMnagementAPIs.Models
{
    public class Order
    {
        [Key] // Marks this property as the primary key in the database
        public int Id { get; set; } // Primary Key

        [Required] // Ensures this property must have a value
        [ForeignKey("User")] // Specifies this property as a foreign key referencing the User table
        public int UserId { get; set; } // Foreign Key to User

        [Required] // Ensures this property must have a value
        public DateTime OrderDate { get; set; } // Date and time the order was placed, e.g., 2024-12-01

        [Required] // Ensures this property must have a value
        [Range(0, double.MaxValue, ErrorMessage = "Total amount cannot be negative.")] // Validates total amount is non-negative
        public decimal TotalAmount { get; set; } // Total amount for the order, e.g., 199.99

        // Relationships
        public Users User { get; set; } // Navigation property to the User who placed the order
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>(); // Products included in the order
    }
}
