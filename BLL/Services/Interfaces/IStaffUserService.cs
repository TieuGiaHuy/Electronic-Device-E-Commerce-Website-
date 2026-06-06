using BLL.DTOs.StaffUser;

namespace BLL.Services.Interfaces
{
    public interface IStaffUserService
    {
        Task<StaffUserDto?> GetByEmailAsync(string email);
        Task<StaffUserDto?> ValidateLoginAsync(LoginDto loginDto);
        Task<StaffUserDto> CreateAsync(StaffUserCreateDto createDto);
    }
}