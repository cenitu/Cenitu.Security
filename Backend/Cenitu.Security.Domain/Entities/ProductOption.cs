namespace Cenitu.Security.Domain.Entities
{
    public class ProductOption
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OptionProductId { get; set; }
        public Product OptionProduct { get; set; }
    }
}