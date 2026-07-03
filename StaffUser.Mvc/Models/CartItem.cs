using System;

namespace StaffUser.Mvc.Models
{
    public class CartItem
    {
        public Guid ProductVariantId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string VariantSku { get; set; } = string.Empty;
        public string VariantColor { get; set; } = string.Empty;
        public string VariantSpecs { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }
        public int MaxStock { get; set; }
    }
}
