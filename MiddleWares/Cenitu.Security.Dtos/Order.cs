using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Dtos
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public ProductListDto Product { get; set; }
    }
}
