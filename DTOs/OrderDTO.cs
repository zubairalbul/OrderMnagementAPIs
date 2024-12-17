namespace OrderMnagementAPIs.DTOs
{
    public class OrderDTO // This DTO transfers simplified order data, focusing on essential fields.
    {
        public int OrderId { get; set; } // Represents the unique identifier for the Order.
        public int UserId { get; set; } // Links the order to the corresponding user.
        public DateTime OrderDate { get; set; } // Captures the date and time the order was placed.
        public decimal TotalAmount { get; set; } // Represents the total cost of the order.
    }
}
