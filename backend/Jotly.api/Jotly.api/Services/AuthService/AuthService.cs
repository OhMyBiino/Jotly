using Jotly.api.DTOs;
using Jotly.api.Models;
using Jotly.api.Repositories.UserRepo;
using Jotly.api.Services.TokenService;

namespace Jotly.api.Services.AuthService
{
    public class AuthService(
        IUserRepository _userRepository,
        ITokenService _tokenService) : IAuthService
    {

        public async Task<AuthResult> LoginAsync(string username, string password) 
        {
            try
            {
                var user = await _userRepository.GetByUsernameAsync(username);
                if (user == null)
                {
                    return new AuthResult() { IsSuccess = false, Errors = { "Invalid Login Credentials" } };
                }

                if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    return new AuthResult() { IsSuccess = false, Errors = { "Invalid Login Credentials" } };
                }

                //get roles

                //generate token passing the roles
                var token = _tokenService.GenerateAccessToken(user);
                //generate refresh token
                var refreshToken = _tokenService.GenerateRefreshToken();


                //return AuthResult
                return new AuthResult()
                {
                    IsSuccess = true,
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddHours(1),
                    User = new UserDto { UserId = user.UserId, Username = user.Username, Email = user.Email }
                };
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<AuthResult> RegisterAsync(CreateUserDto request) 
        {
            try
            {
                var user = await _userRepository.GetByUsernameAsync(request.Username);

                if (user != null)
                    return new AuthResult() { IsSuccess = false, Errors = { "Username already used." } };

                var newUser = new User()
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
                };

                var createdUser = await _userRepository.CreateAsync(newUser);

                var token = _tokenService.GenerateAccessToken(newUser);
                var refreshToken = _tokenService.GenerateRefreshToken();

                return new AuthResult()
                {
                    IsSuccess = true,
                    Token = token,
                    RefreshToken = refreshToken,
                    ExpiresAt = DateTime.UtcNow.AddHours(1),
                    User = new UserDto 
                    {
                        UserId = createdUser.UserId,
                        Username = createdUser.Username,
                        Email = createdUser.Email,
                    }
                };
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
