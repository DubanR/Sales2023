using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class SaleDetail
    {
        public int id { get; set; }

        public Sale? sale { get; set; }

        public int saleId { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? remarks { get; set; }

        public Product? product { get; set; }

        public int productId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor")]
        public decimal Value => product == null ? 0 : (decimal)quantity * product.price;
    }
}
