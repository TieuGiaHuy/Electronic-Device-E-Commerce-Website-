namespace BLL.DTOs.Product
{
    public class ProductUpdateDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public int CategoryId { get; set; }
    }
}
