namespace Sales.Shared.Entities
{
    public class ProductCategory
    {
        public int id { get; set; }

        public Product product { get; set; } = null!;

        public int productId { get; set; }

        public Category category { get; set; } = null!;

        public int categoryId { get; set; }
    }
}
