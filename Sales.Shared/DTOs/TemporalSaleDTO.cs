namespace Sales.Shared.DTOs
{
    public class TemporalSaleDTO
    {
        public int productId { get; set; }

        public float quantity { get; set; } = 1;

        public string remarks { get; set; } = string.Empty;
    }
}
