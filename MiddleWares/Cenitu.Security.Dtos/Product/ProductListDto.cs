namespace Cenitu.Security.Dtos.Product
{
    public class ProductListDto:TracedBaseDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;

        public string PrimaryUnitSymbol { get; set; } =default!;      
     
    }
}
