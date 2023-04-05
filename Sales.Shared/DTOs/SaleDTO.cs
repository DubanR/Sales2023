using Sales.Shared.Enums;

namespace Sales.Shared.DTOs
{
    public class SaleDTO
    {
        public int id { get; set; }

        public OrderStatus orderStatus { get; set; }

        public string remarks { get; set; } = string.Empty;
    }
}
