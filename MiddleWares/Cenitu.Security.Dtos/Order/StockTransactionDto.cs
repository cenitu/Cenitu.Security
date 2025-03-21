using Cenitu.Security.Dtos.Enums;

namespace Cenitu.Security.Dtos.Order
{
    public class StockTransactionDto
    {
        public DateTime Date { get; set; }
        
        public StockTransactionStatus Status { get; set; }
        public TransactionSource TransactionSource { get; set; }
        public List<StockTransactionLineDto> StockTransactions { get; set; } = [];
    }

}
