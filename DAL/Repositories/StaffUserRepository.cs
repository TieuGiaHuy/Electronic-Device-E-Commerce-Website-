using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class StaffUserRepository : IStaffUserRepository
    {
        private readonly AppDbContext _context;
        public StaffUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StaffUser>> GetAllAsync()
        {
            return await _context.StaffUsers.ToListAsync();
        }

        public async Task<StaffUser?> GetByIdAsync(int staffUserId)
        {
            return await _context.StaffUsers.FindAsync(staffUserId);
        }

        public async Task<StaffUser?> GetByEmailAsync(string email)
        {
            return await _context.StaffUsers.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<StaffUser> CreateAsync(StaffUser staffUser)
        {
            _context.StaffUsers.Add(staffUser);
            await _context.SaveChangesAsync();
            return staffUser;
        }

        public async Task<bool> UpdateAsync(StaffUser staffUser)
        {
            var existing = await _context.StaffUsers.FindAsync(staffUser.StaffUserId);
            if (existing != null)
            {
                existing.Update(staffUser.FullName, staffUser.Role);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int staffUserId)
        {
            var staffUser = await _context.StaffUsers.FirstOrDefaultAsync(s => s.StaffUserId == staffUserId);
            if (staffUser != null)
            {
                staffUser.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> SetPasswordAsync(int staffUserId, string passwordHash)
        {
            var staffUser = await _context.StaffUsers.FindAsync(staffUserId);
            if (staffUser != null)
            {
                staffUser.SetPassword(passwordHash);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}