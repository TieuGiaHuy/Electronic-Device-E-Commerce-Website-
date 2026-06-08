using BLL.DTOs.Staff;

namespace BLL.Services.Interfaces
{
    public interface IStaffService
    {
        Task<StaffDto?> GetByEmailAsync(string email);
        Task<StaffDto?> ValidateLoginAsync(string email, string password);
        Task<StaffDto> CreateAsync(StaffCreateDto createDto);
    }
}