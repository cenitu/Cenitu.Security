using Cenitu.Security.Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.Design;
using System.IO.Pipes;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace Cenitu.Security.BlazorWebAssembly.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IAccountManagement
    {
        private bool _authinticated = false;
        private readonly ClaimsPrincipal Unauthenticated = new(new ClaimsIdentity());
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public CustomAuthenticationStateProvider(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("Auth");
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _authinticated = false;
            var user = Unauthenticated;
            try
            {
                var userResponse = await httpClient.GetAsync("manage/info");
                userResponse.EnsureSuccessStatusCode();
                var userJson = await userResponse.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<UserInfo>(userJson, jsonOptions);
                if (userInfo != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userInfo.Email),
                        new Claim(ClaimTypes.Email, userInfo.Email),

                    };
                    claims.AddRange(userInfo.Claims.Where(c => c.Key != ClaimTypes.Name && c.Key != ClaimTypes.Email).Select(c => new Claim(c.Key, c.Value)));
                    var rolesResponse = await httpClient.GetAsync($"/api/Role/GetUserRole?emailId={userInfo.Email}");
                    rolesResponse.EnsureSuccessStatusCode();
                    var rolesJson = await rolesResponse.Content.ReadAsStringAsync();
                    var roles = JsonSerializer.Deserialize<string[]>(rolesJson, jsonOptions);
                    if (roles != null && roles.Length > 0)
                    {
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }
                    }
                    var id = new ClaimsIdentity(claims, nameof(CustomAuthenticationStateProvider));
                    user = new ClaimsPrincipal(id);
                    _authinticated = true;
                }
            }
            catch (Exception ex)
            {

                
            }

            return new AuthenticationState(user);
        }

        public async Task<FormResult> RegisterAsync(string email, string password)
        {
            string[] defaultDetail = ["An unknown error prevented registration from succeeding."];
            try
            {
                var response = await httpClient.PostAsJsonAsync("register", new { Email = email, Password = password });
                if (response.IsSuccessStatusCode)
                {
                    return new FormResult { Succeeded = true };
                }
                var details = await response.Content.ReadAsStringAsync();
                var problemDetails = JsonDocument.Parse(details);
                var errors = new List<string>();
                var errorList = problemDetails.RootElement.GetProperty("errors");
                foreach (var error in errorList.EnumerateObject())
                {
                    if (errorList.ValueKind == JsonValueKind.String)
                    {
                        errors.Add(error.Value.GetString()!);
                    }
                    else if (errorList.ValueKind == JsonValueKind.Array)
                    {
                        errors.AddRange(error.Value.EnumerateArray().Select(e => e.GetString() ?? string.Empty).Where(e => !string.IsNullOrEmpty(e)));
                    }


                }
                return new FormResult { Succeeded = false, ErrorList = problemDetails == null ? defaultDetail : [.. errors] };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<FormResult> LoginAsync(string email, string password)
        {

            try
            {
                var response = await httpClient.PostAsJsonAsync("login?useCookies=true", new
                {
                    Email = email,
                    Password = password
                });
                if (response.IsSuccessStatusCode)
                {
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    return new FormResult { Succeeded = true };
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return new FormResult { Succeeded = false, ErrorList = ["Invalid email and/or password."] };

        }
    }
}
