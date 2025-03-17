using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Domain.Entities
{
    public class ProductionOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = default!;
        public DateTime Date { get; set; }
        public int ProductId { get; set; } 
        public int Quantity { get; set; } 
        public Product Product { get; set; }
        public int StockTransactionId { get; set; }
        public StockTransaction StockTransaction { get; set; } 
    }
}
