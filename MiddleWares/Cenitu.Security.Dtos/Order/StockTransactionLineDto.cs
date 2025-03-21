using Cenitu.Security.Dtos.Enums;
using Cenitu.Security.Dtos.Product;

namespace Cenitu.Security.Dtos.Order
{
    public class StockTransactionLineDto
    {
        public int ProductId { get; set; }
        public decimal TransactionUnitQuantity { get; set; }
        public int UnitId { get; set; }
        public decimal Price { get; set; } = 0;
        public TransactionType TransactionType { get; set; }
        public int StockTransactionId { get; set; }
    }

}
