namespace Cenitu.Security.Dtos.Order
{
    public class StockTransactionDto
    {
        public DateTime Date { get; set; }
        public List<StockTransactionLineDto> StockTransactions { get; set; } = [];
    }

}
