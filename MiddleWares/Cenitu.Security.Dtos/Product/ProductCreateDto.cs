using Cenitu.Security.Dtos.Enums;

namespace Cenitu.Security.Dtos.Product
{
    public class ProductCreateDto:TracedBaseDto
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProductType ProductType { get; set; }
        public List<ProductUnitDto> ProductUnits { get; set; } = new();
    }

}
