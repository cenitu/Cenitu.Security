using Cenitu.Security.Dtos.Enums;

namespace Cenitu.Security.Domain.Entities
{
    public class StockTransaction
    {
        public int Id { get; set; }
        public TransactionSource TransactionSource { get; set; }
        public DateTime Date { get; set; }
        public ICollection<StockTransactionLine> StockTransactions { get; set; } = [];
    }



}
