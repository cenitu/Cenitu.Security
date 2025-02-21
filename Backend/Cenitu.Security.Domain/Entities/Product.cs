using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; } 
        public ICollection<Order>?  Orders { get; set; }
        public ICollection<ProductUnit>? ProductUnits { get; set; }

        public ProductUnit? PrimaryUnit
        {
            get
            {
                return ProductUnits.FirstOrDefault(pu => pu.IsPrimary);
            }
        }

         public string? PrimaryUnitSymbol => ProductUnits.FirstOrDefault(pu => pu.IsPrimary)?.Unit?.Symbol;  
    }
}
