namespace OrderMnagementAPIs.DTOs
{
    public class ProductsDTO // This DTO is used to transfer product data to clients in a simplified form.
    {
        public int ProductId { get; set; } // Represents the unique identifier for the Product.
        public string ProductName { get; set; } // Contains the product name.
        public string Description { get; set; } // Provides an optional description of the product.
        public decimal ProductPrice { get; set; } // Holds the price of the product.
        public int ProductStock { get; set; } // Indicates the available stock quantity for the product.
        public decimal OverallRating { get; set; } // Displays the overall rating calculated from reviews.
    }
}
