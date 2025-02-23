namespace Cenitu.Security.Dtos.Product
{
    public class ProductUnitDto
    {
        public int UnitId { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public decimal ConversionFactor { get; set; }
    }
}