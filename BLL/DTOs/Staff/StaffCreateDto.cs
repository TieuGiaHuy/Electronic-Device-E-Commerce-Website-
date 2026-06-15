namespace BLL.DTOs.Staff
{
    public class StaffCreateDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public bool IsActive { get; set; } = true;
    }
}