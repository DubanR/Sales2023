using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class TemporalSale
    {
        public int id { get; set; }

        public User? user { get; set; }

        public string? userId { get; set; }

        public Product? product { get; set; }

        public int productId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float quantity { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? remarks { get; set; }

        public decimal Value => product == null ? 0 : product.price * (decimal)quantity;
    }
}
