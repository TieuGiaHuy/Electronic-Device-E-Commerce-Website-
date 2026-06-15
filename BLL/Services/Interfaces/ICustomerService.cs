using BLL.DTOs.Customer;

namespace BLL.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetByEmailAsync(string email);
        Task<CustomerDto?> ValidateLoginAsync(string email, string password);
        Task<CustomerDto> CreateAsync(CustomerCreateDto createDto);
        Task<bool> SetPasswordAsync(int customerId, string newPassword);
    }
}