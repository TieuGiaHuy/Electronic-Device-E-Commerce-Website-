using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class StaffUserConfiguration : IEntityTypeConfiguration<StaffUser>
    {
        public void Configure(EntityTypeBuilder<StaffUser> builder)
        {
            builder.HasKey(s => s.StaffUserId);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(255);
            builder.HasIndex(s => s.Email).IsUnique();
            builder.Property(s => s.FullName).IsRequired().HasMaxLength(100);
            builder.Property(s => s.PasswordHash).IsRequired();
            builder.Property(s => s.Role).IsRequired().HasMaxLength(50);
            builder.Property(s => s.IsActive).IsRequired();

            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}