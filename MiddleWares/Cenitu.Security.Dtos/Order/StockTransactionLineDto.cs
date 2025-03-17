using Cenitu.Security.Dtos.Enums;

namespace Cenitu.Security.Dtos.Order
{
    public class StockTransactionLineDto
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; } = 0;
        public TransactionType TransactionType { get; set; }
        public int StockTransactionId { get; set; }
    }

}
