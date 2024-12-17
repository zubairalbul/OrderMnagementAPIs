namespace OrderMnagementAPIs.DTOs
{
    public class UsersDTO // This DTO is used to transfer lightweight User data, excluding sensitive information like passwords.
    {
        public int UserId { get; set; } // Represents the unique identifier for the User.
        public string UserName { get; set; } // Contains the user's name.
        public string UserEmail { get; set; } // Stores the user's email address.
        public string UserPhone { get; set; } // Holds the user's phone number.
        public string Role { get; set; } // Represents the role of the user, such as Admin or Customer.
        public DateTime CreatedAt { get; set; } // Indicates the creation date of the user.
    }
}
