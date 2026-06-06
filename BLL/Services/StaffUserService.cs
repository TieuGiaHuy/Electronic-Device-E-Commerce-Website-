using BLL.DTOs.StaffUser;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class StaffUserService : IStaffUserService
    {
        private readonly IStaffUserRepository _staffUserRepository;

        public StaffUserService(IStaffUserRepository staffUserRepository)
        {
            _staffUserRepository = staffUserRepository;
        }

        public async Task<StaffUserDto?> GetByEmailAsync(string email)
        {
            var staffUser = await _staffUserRepository.GetByEmailAsync(email);
            if (staffUser == null) return null;

            return new StaffUserDto
            {
                StaffUserId = staffUser.StaffUserId,
                Email = staffUser.Email,
                FullName = staffUser.FullName,
                Role = staffUser.Role,
                IsActive = staffUser.IsActive,
                CreatedAt = staffUser.CreatedAt,
                UpdatedAt = staffUser.UpdatedAt
            };
        }

        public async Task<StaffUserDto?> ValidateLoginAsync(LoginDto loginDto)
        {
            var staffUser = await _staffUserRepository.GetByEmailAsync(loginDto.Email);
            if (staffUser == null || !staffUser.IsActive) return null;

            // Verify password hash using BCrypt
            var verified = BCrypt.Net.BCrypt.Verify(loginDto.Password, staffUser.PasswordHash);
            if (!verified) return null;

            return new StaffUserDto
            {
                StaffUserId = staffUser.StaffUserId,
                Email = staffUser.Email,
                FullName = staffUser.FullName,
                Role = staffUser.Role,
                IsActive = staffUser.IsActive,
                CreatedAt = staffUser.CreatedAt,
                UpdatedAt = staffUser.UpdatedAt
            };
        }

        public async Task<StaffUserDto> CreateAsync(StaffUserCreateDto createDto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(createDto.Password);
            var staffUser = StaffUser.Create(createDto.Email, createDto.FullName, passwordHash, createDto.Role);
            var created = await _staffUserRepository.CreateAsync(staffUser);

            return new StaffUserDto
            {
                StaffUserId = created.StaffUserId,
                Email = created.Email,
                FullName = created.FullName,
                Role = created.Role,
                IsActive = created.IsActive,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }
    }
}