using Microsoft.AspNetCore.Identity;

namespace Cenitu.Security.Domain.Entities
{
    
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}

