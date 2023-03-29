using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class ProductImage
    {
        public int id { get; set; }

        public Product product { get; set; } = null!;

        public int productId { get; set; }

        [Display(Name = "Imagen")]
        public string image { get; set; } = null!;
    }
}