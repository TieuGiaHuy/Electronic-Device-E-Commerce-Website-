namespace DAL.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; }
        virtual public Category Category { get; set; }
    }
}
