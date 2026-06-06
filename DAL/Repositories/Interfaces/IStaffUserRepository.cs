using DAL.Models;

namespace DAL.Repositories.Interfaces
{
    public interface IStaffUserRepository
    {
        Task<List<StaffUser>> GetAllAsync();
        Task<StaffUser?> GetByIdAsync(int staffUserId);
        Task<StaffUser?> GetByEmailAsync(string email);
        Task<StaffUser> CreateAsync(StaffUser staffUser);
        Task<bool> UpdateAsync(StaffUser staffUser);
        Task<bool> DeleteAsync(int staffUserId);
        Task<bool> SetPasswordAsync(int staffUserId, string passwordHash);
    }
}