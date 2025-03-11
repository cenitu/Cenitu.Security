namespace Cenitu.Security.Dtos.Product
{
    public class ProductUnitDto 
    {
        public int UnitId { get; set; }
        public string UnitSymbol { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
        public decimal ConversionFactor { get; set; }
        public int ProductId { get; set; }
    }
}
