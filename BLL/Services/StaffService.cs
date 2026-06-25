using BLL.DTOs.Staff;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class StaffService : IStaffService
    {
                /*
                 StaffService (BLL)
                 - Handles staff-related operations used by Admin.Blazor and UserStaff.Razor
                 - Methods: GetByEmailAsync, ValidateLoginAsync (BCrypt.Verify), CreateAsync (hashes),
                     and SetPasswordAsync (hash+update)
                 - Delegates data access to IStaffRepository
                */
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task<StaffDto?> GetByEmailAsync(string email)
        {
            // Lookup staff by email and map to DTO for UI layers
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
            // Validate login by verifying password hash
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
            // Hash password and create staff entity via repository
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
            // Hash and update password via repository
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            return await _staffRepository.SetPasswordAsync(staffId, passwordHash);
        }

        public async Task<bool> UpdateProfileAsync(int staffId, StaffProfileDto profileDto)
        {
            var staff = await _staffRepository.GetByIdAsync(staffId);
            if (staff == null) return false;

            staff.FullName = profileDto.FullName;
            staff.UpdatedAt = DateTime.UtcNow;

            return await _staffRepository.UpdateAsync(staff);
        }
    }
}