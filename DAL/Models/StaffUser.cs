using DAL.Models.Base;

namespace DAL.Models
{
    public class StaffUser : IBaseEntity
    {
        public int StaffUserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static StaffUser Create(string email, string fullName, string passwordHash, string role = "Staff")
        {
            return new StaffUser
            {
                Email = email,
                FullName = fullName,
                PasswordHash = passwordHash,
                Role = role,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(string fullName, string role)
        {
            FullName = fullName;
            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string passwordHash)
        {
            PasswordHash = passwordHash;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}