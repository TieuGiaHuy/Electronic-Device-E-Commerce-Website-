using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    public class VariantAttributeConfiguration : IEntityTypeConfiguration<VariantAttribute>
    {
        public void Configure(EntityTypeBuilder<VariantAttribute> builder)
        {
            builder.HasKey(a => a.VariantAttributeId);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Value).IsRequired().HasMaxLength(200);
            builder.Property(a => a.DisplayOrder).IsRequired();

            builder.HasOne(a => a.ProductVariant)
                   .WithMany(pv => pv.Attributes)
                   .HasForeignKey(a => a.ProductVariantId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Khớp với bộ lọc xóa mềm của ProductVariant (tránh cảnh báo EF 10622)
            builder.HasQueryFilter(a => !a.ProductVariant!.IsDeleted);

            builder.HasIndex(a => a.ProductVariantId).HasDatabaseName("IX_VariantAttribute_ProductVariantId");
        }
    }
}
