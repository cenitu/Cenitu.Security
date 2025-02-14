using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cenitu.Security.Dtos
{
    public class UserInfo
    {
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
