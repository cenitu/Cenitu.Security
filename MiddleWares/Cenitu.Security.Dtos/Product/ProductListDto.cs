using Cenitu.Security.Dtos.Enums;

namespace Cenitu.Security.Dtos.Product
{
    public class ProductListDto : TracedBaseDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PrimaryUnitSymbol { get; set; } = default!;
        public decimal StockQuantity { get; set; } = 0;
        public bool CanBeAnOption { get; set; }
        public decimal SellingPrice { get; set; }
        public List<ProductUnitDto> ProductUnits { get; set; } = new();
        public ProductType ProductType { get; set; }
        public ProductTrackingType ProductTrackingType { get; set; }

    }
}
