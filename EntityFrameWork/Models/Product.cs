namespace EntityFrameWork.Models
{
    public class Product:BaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Count { get; set; }
        public decimal Price { get; set; }

        public ICollection <ProductImage> Images { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
