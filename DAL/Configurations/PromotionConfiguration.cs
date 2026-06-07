using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Promotion> builder)
        {
            builder.HasKey(p => p.PromotionId);
            builder.Property(p => p.SalePrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.MaxReservedStock).IsRequired();
            builder.Property(p => p.StartAt).IsRequired();
            builder.Property(p => p.EndAt).IsRequired();
            // Thiết lập quan hệ với ProductVariant
            builder.HasOne(p => p.ProductVariant)
                   .WithMany()
                   .HasForeignKey(p => p.ProductVariantId)
                   .OnDelete(DeleteBehavior.Cascade);
            // Check xóa mềm
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
