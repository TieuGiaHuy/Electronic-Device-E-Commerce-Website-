using DAL.Models.Base;

namespace DAL.Models
{
    public class ProductReview : IBaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; } // 1 to 5 stars
        public string Comment { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }

        public static ProductReview Create(int userId, int productId, int rating, string comment)
        {
            return new ProductReview
            {
                UserId = userId,
                ProductId = productId,
                Rating = rating,
                Comment = comment,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };
        }

        public void Update(int rating, string comment)
        {
            Rating = rating;
            Comment = comment;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}