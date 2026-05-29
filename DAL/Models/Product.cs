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

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
