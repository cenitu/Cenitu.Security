using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Dtos
{
    public class AddUserModel
    {
        public string UserEmail { get; set; }
        public string[] Roles { get; set; }
    }
}
