using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // 1. Cấu hình tên bảng nếu muốn khác tên DbSet
            builder.ToTable("Categories");

            // 2. Cấu hình Khóa chính và các ràng buộc cột
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

            builder.HasQueryFilter(p => !p.IsDeleted);

            // db seed data
            builder.HasData(
                new Category { Id = 1, Name = "Electronics", IsDeleted = false, CreatedAt = DateTime.UtcNow },
                new Category { Id = 2, Name = "Books", IsDeleted = false, CreatedAt = DateTime.UtcNow },
                new Category { Id = 3, Name = "Clothing", IsDeleted = false, CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
