using Cenitu.Security.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Domain.Entities
{
    public class StockTransactionLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public TransactionType TransactionType { get; set; }
      
        public int StockTransactionId { get; set; }
        public StockTransaction StockTransaction { get; set; }



    }


    public class StockTransaction
    {
        public int Id { get; set; }
        public TransactionSource TransactionSource { get; set; }
        public ICollection<StockTransactionLine> StockTransactions { get; set; } = [];
    }



}
