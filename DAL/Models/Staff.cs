using DAL.Enums;
using DAL.Models.Base;

namespace DAL.Models
{
    public class Staff : IBaseEntity
    {
        public int StaffId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public RoleEnum Role { get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
