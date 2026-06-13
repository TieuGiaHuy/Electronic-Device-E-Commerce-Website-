using BLL.DTOs.Staff;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<StaffDto?> GetByEmailAsync(string email)
        {
            var staff = await _staffRepository.GetByEmailAsync(email);
            if (staff == null) return null;

            return new StaffDto
            {
                StaffId = staff.StaffId,
                FullName = staff.FullName,
                Email = staff.Email,
                Role = (int)staff.Role,
                IsActive = staff.IsActive,
                CreatedAt = staff.CreatedAt,
                UpdatedAt = staff.UpdatedAt
            };
        }

        public async Task<StaffDto?> ValidateLoginAsync(string email, string password)
        {
            var staff = await _staffRepository.GetByEmailAsync(email);
            if (staff == null) return null;

            var verified = BCrypt.Net.BCrypt.Verify(password, staff.PasswordHash);
            if (!verified) return null;

            return new StaffDto
            {
                StaffId = staff.StaffId,
                FullName = staff.FullName,
                Email = staff.Email,
                Role = (int)staff.Role,
                IsActive = staff.IsActive,
                CreatedAt = staff.CreatedAt,
                UpdatedAt = staff.UpdatedAt
            };
        }

        public async Task<StaffDto> CreateAsync(StaffCreateDto createDto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(createDto.Password);
            var staff = new Staff
            {
                FullName = createDto.FullName,
                Email = createDto.Email,
                PasswordHash = passwordHash,
                Role = (DAL.Enums.RoleEnum)createDto.Role,
                IsActive = createDto.IsActive,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };
            var created = await _staffRepository.CreateAsync(staff);

            return new StaffDto
            {
                StaffId = created.StaffId,
                FullName = created.FullName,
                Email = created.Email,
                Role = (int)created.Role,
                IsActive = created.IsActive,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };
        }

        public async Task<bool> SetPasswordAsync(int staffId, string newPassword)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            return await _staffRepository.SetPasswordAsync(staffId, passwordHash);
        }
    }
}