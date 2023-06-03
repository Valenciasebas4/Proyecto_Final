namespace Proyecto_Final.Models
{
    public class OrderSuccessViewModel
    {
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public float? Quantity { get; set; }

        public decimal? Total => Price == null ? 0 : Price * (decimal)Quantity;
    }
}
