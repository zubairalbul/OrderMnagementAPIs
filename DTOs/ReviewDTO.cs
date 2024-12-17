namespace OrderMnagementAPIs.DTOs
{
   
    public class ReviewDTO // This DTO transfers review data, including ratings and optional comments.
    {
        public int ReviewId { get; set; } // Represents the unique identifier for the Review.
        public int UserId { get; set; } // Links the review to the corresponding user.
        public int ProductId { get; set; } // Links the review to the corresponding product.
        public int Rating { get; set; } // Stores the rating given by the user (1 to 5).
        public string Comment { get; set; } // Optional comment provided by the user.
        public DateTime ReviewDate { get; set; } // Records the date and time the review was submitted.
    }
}
