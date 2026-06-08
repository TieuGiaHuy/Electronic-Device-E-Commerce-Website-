namespace BLL.DTOs.Customer
{
    public class CustomerCreateDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public int Role { get; set; } = 1; // Customer by default
        public int Points { get; set; } = 0;
    }
}