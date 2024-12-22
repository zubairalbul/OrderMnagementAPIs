namespace OrderMnagementAPIs.Models
{
    public class Cart
    {
        public int CartId { get; set; } // Primary key
        public int UserId { get; set; } // Foreign key to associate with a user
        public List<CartItem> Items { get; set; } = new List<CartItem>(); // List of items in the cart
    }

    public class CartItem
    {
        public int CartItemId { get; set; } // Primary key
        public int CartId { get; set; } // Foreign key to associate with a cart
        public int ProductId { get; set; } // Foreign key to associate with a product
        public int Quantity { get; set; } // Quantity of the product in the cart
    }

}
