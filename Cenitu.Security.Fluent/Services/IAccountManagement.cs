using Cenitu.Security.Dtos;

namespace Cenitu.Security.Fluent.Services
{
    public interface IAccountManagement
    {
        public Task<FormResult> RegisterAsync(string email, string password);
        public Task<FormResult> LoginAsync(string email, string password);
        public Task LogoutAsync();
        Task<bool> CheckAuthenticatedAsync();
    }
}
