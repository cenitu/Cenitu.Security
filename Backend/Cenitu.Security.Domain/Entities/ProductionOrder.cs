using Cenitu.Security.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Domain.Entities
{
    public class ProductionOrder
    {
        public TransactionSource TransactionSource = TransactionSource.Production;
        public int Id { get; set; }
        public string OrderNumber { get; set; } = default!;
        public DateTime Date { get; set; } 
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal MaterialCost
        {
            get
            {
                return StockTransaction.StockTransactions.Where(st => st.TransactionType == TransactionType.Input).Sum(st => st.Price * st.TransactionUnitQuantity);
            }

        }
        public Product Product { get; set; }
        public int StockTransactionId { get; set; }
        public StockTransaction StockTransaction { get; set; }
    }
}
