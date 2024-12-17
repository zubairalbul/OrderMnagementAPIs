using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrderMnagementAPIs.Models
{
    public class OrderProduct
    {
        [Key] // Composite key part 1
        [ForeignKey("Order")] // Specifies this property as a foreign key referencing the Order table
        public int OrderId { get; set; } // Foreign Key to Order

        [Key] // Composite key part 2
        [ForeignKey("Product")] // Specifies this property as a foreign key referencing the Product table
        public int ProductId { get; set; } // Foreign Key to Product

        [Required] // Ensures this property must have a value
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")] // Validates quantity is at least 1
        public int Quantity { get; set; } // Number of items for the product in the order, e.g., 2

        // Relationships
        public Order Order { get; set; } // Navigation property to the Order
        public Products Product { get; set; } // Navigation property to the Product
    }
}
