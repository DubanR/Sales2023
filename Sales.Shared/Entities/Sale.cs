using Sales.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class Sale
    {
        public int id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        [Display(Name = "Fecha/Hora")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime date { get; set; }

        public User? user { get; set; }

        public string? userId { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? remarks { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Display(Name = "Líneas")]
        public int Lines => SaleDetails == null ? 0 : SaleDetails.Count;

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        public float quantity => SaleDetails == null ? 0 : SaleDetails.Sum(sd => sd.quantity);

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Valor")]

        public decimal Value => SaleDetails == null ? 0 : SaleDetails.Sum(sd => sd.Value);
    }
}
