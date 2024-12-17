using System.ComponentModel.DataAnnotations;

namespace OrderMnagementAPIs.Models
{
    public class Review
    {
        [Key] // Marks this property as the primary key in the database
        public int ReviewId { get; set; } // Primary Key

        [Required] // Ensures this property must have a value
        public int UserId { get; set; } // Foreign Key to User

        [Required] // Ensures this property must have a value
        public int ProductId { get; set; } // Foreign Key to Product

        [Required] // Ensures this property must have a value
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")] // Validates rating is within range
        public int Rating { get; set; } // Rating given by the user, e.g., 4

        public string Comment { get; set; } // Optional comment about the product, e.g., "Great product!"

        [Required] // Ensures this property must have a value
        public DateTime ReviewDate { get; set; } // Date and time the review was written, e.g., 2024-12-01

        // Relationships
        public Users User { get; set; } // Navigation property to the User who wrote the review
        public Products Product { get; set; } // Navigation property to the Product being reviewed
    }
}
