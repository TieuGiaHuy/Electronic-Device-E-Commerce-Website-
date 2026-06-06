using BLL.DTOs.User;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetByEmailAsync(string email);
        Task<UserDto?> ValidateLoginAsync(string email, string password);
        Task<UserDto> CreateAsync(UserCreateDto createDto);
    }
}