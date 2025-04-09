namespace Cenitu.Security.Dtos.Product
{
    public class ProductOptionDto
    {
        public int ProductId { get; set; }
        public string ProductDescription { get; set; }=string.Empty;
        public int OptionProductId { get; set; }
        public string OptionProductDescription { get; set; } = string.Empty;
    }
}