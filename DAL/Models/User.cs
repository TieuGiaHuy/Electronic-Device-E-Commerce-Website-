using DAL.Models.Base;

namespace DAL.Models
{
    public class User : IBaseEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public int Points { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static User Create(string fullName, string email, string phoneNumber, string passwordHash, UserRole role = UserRole.Customer, int points = 0)
        {
            return new User
            {
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                PasswordHash = passwordHash,
                Role = role,
                Points = points,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(string fullName, string phoneNumber, UserRole role, int points)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Role = role;
            Points = points;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string passwordHash)
        {
            PasswordHash = passwordHash;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddPoints(int points)
        {
            Points += points;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}