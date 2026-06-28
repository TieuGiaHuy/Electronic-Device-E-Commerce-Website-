using BLL.DTOs.User;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        /*
         UserService (BLL)
         - Similar responsibilities as CustomerService but for app 'users'
         - Exposes GetByEmailAsync, ValidateLoginAsync, CreateAsync
         - Uses IUserRepository for data access and BCrypt for hashing/verification
        */
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            // Lookup user entity by email and map to DTO
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = (int)user.Role,
                Points = user.Points,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<UserDto?> ValidateLoginAsync(string email, string password)
        {
            // Validate credentials using BCrypt.Verify against stored password hash
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return null;

            var verified = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!verified) return null;

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = (int)user.Role,
                Points = user.Points,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<UserDto> CreateAsync(UserCreateDto createDto)
        {
            // Hash password and create user via repository
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(createDto.Password);
            var user = User.Create(createDto.FullName, createDto.Email, createDto.PhoneNumber, passwordHash, (UserRole)createDto.Role, createDto.Points);
            var created = await _userRepository.CreateAsync(user);

            return new UserDto
            {
                Id = created.Id,
                FullName = created.FullName,
                Email = created.Email,
                PhoneNumber = created.PhoneNumber,
                Role = (int)created.Role,
                Points = created.Points,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }
    }
}