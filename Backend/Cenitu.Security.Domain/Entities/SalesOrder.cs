namespace Cenitu.Security.Domain.Entities
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public int StockTransactionId { get; set; }
        public StockTransaction StockTransaction { get; set; } = new StockTransaction();
    }
}
