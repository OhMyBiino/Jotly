using Jotly.api.DTOs;

namespace Jotly.api.Services.AuthService
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(string username, string password);
        Task<AuthResult> RegisterAsync(CreateUserDto request);
    }
}
