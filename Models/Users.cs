using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderMnagementAPIs.Models
{

    public class Users
    {
        [Key] // Marks this property as the primary key in the database
        public int Id { get; set; } // Primary Key

        [Required] // Ensures this property must have a value
        public string Name { get; set; } // User's name, required field

        [Required] // Ensures email is mandatory
        [EmailAddress] // Validates the property follows email format
        public string Email { get; set; } // User's email address, must be unique

        [Required] // Ensures password is mandatory
        public string Password { get; set; } // User's hashed password

        [Required] // Ensures phone number is mandatory
        [Phone] // Validates the property follows a phone number format
        public string Phone { get; set; } // User's phone number, e.g., +1234567890

        [Required] // Ensures role is mandatory
        public string Role { get; set; } // User's role, e.g., Admin or Customer

        public DateTime CreatedAt { get; set; } // Date and time the user was created

        // Relationships
         public ICollection<Order> Orders { get; set; } = new List<Order>(); // A user can place multiple orders
        // public ICollection<Review> Reviews { get; set; } = new List<Review>(); // A user can write multiple reviews

    }
}
