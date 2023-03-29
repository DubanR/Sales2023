using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class Category
    {
        public int id { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Categoria")]
        public string name { get; set; } = null!;

        public ICollection<ProductCategory>? ProductCategories { get; set; }

        [Display(Name = "Productos")]
        public int ProductCategoriesNumber => ProductCategories == null ? 0 : ProductCategories.Count;
    }
}
