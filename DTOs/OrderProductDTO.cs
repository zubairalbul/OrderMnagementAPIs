namespace OrderMnagementAPIs.DTOs
{
    public class OrderProductDTO // This DTO manages the many-to-many relationship between Orders and Products.
    {
        public int OrderId { get; set; } // References the unique identifier for the Order.
        public int ProductId { get; set; } // References the unique identifier for the Product.
        public int Quantity { get; set; } // Specifies the number of products in the order.
    }

}
