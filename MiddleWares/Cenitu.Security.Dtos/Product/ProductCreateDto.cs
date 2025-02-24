using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Dtos.Product
{
    public class ProductCreateDto
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        //public int PrimaryUnitId { get; set; }  // Ürünün birincil birimi
        public List<ProductUnitDto> ProductUnits { get; set; } = new();
    }

}
