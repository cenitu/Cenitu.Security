using Cenitu.Security.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Dtos.Order
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = default!;
        public DateTime Date { get; set; }
        public string ProductCode { get; set; } = default!;
        public int ProductId { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public decimal MaterialCost { get; set; } = 0;
        public string ProductDescription { get; set; } = default!;
        public StockTransactionStatus StockTransactionStatus { get; set; }
        //public StockTransactionDto StockTransaction { get; set; } 
    }
}
