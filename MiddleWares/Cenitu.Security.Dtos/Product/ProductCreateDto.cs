namespace Cenitu.Security.Dtos.Product
{
    public class ProductCreateDto:TracedBaseDto
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ProductUnitDto> ProductUnits { get; set; } = new();
    }

}
