using Jotly.api.Models;

namespace Jotly.api.Services.TokenService
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
