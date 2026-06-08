using BLL.DTOs.Customer;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto?> GetByEmailAsync(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            if (customer == null) return null;

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Role = (int)customer.Role,
                Points = customer.Points,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }

        public async Task<CustomerDto?> ValidateLoginAsync(string email, string password)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            if (customer == null) return null;

            var verified = BCrypt.Net.BCrypt.Verify(password, customer.PasswordHash);
            if (!verified) return null;

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Role = (int)customer.Role,
                Points = customer.Points,
                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            };
        }

        public async Task<CustomerDto> CreateAsync(CustomerCreateDto createDto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(createDto.Password);
            var customer = new Customer
            {
                FullName = createDto.FullName,
                Email = createDto.Email,
                PhoneNumber = createDto.PhoneNumber,
                PasswordHash = passwordHash,
                Role = (UserRole)createDto.Role,
                Points = createDto.Points,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };
            var created = await _customerRepository.CreateAsync(customer);

            return new CustomerDto
            {
                CustomerId = created.CustomerId,
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