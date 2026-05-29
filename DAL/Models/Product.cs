using DAL.Models.Base;
using Microsoft.VisualBasic;

namespace DAL.Models
{
    public class Product : IBaseEntity
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string Brand { get; set; }

        public int CategoryId { get; set; }
        virtual public Category Category { get; set; }
        virtual public ICollection<ProductVariant> ProductVariants { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static Product Create(string productName, string brand, int categoryId) {
            return new Product {
                ProductName = productName,
                Brand = brand,
                CategoryId = categoryId,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Delete() {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(string productName, string brand, int categoryId) {
            ProductName = productName;
            Brand = brand;
            CategoryId = categoryId;

            UpdatedAt = DateTime.UtcNow;
        }
    }
}
