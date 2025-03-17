using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Dtos.Order
{
    public class ProductionOrderCreateDto
    {
        public string OrderNumber { get; set; } = default!;
        public DateTime Date { get; set; }=DateTime.Now;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int StockTransactionId { get; set; }
        public StockTransactionDto StockTransaction { get; set; } = new();


    }

}
