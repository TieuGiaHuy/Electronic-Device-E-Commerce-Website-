using DAL.Models.Base;
using Microsoft.VisualBasic;

namespace DAL.Models
{
    public class Category : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
