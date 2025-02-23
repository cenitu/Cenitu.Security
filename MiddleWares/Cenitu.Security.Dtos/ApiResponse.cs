using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Dtos
{
    public class ApiResponse<T> where T : class
    {
        
        public List<T> Items { get; set; }
        public int Count { get; set; }
    }
}
