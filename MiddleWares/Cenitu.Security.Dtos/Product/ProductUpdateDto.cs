using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Dtos.Product
{
    public class ProductUpdateDto : ProductCreateDto
    {
        public int Id { get; set; }
    }
}
