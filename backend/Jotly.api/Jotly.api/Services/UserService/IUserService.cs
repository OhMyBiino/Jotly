using Jotly.api.DTOs;

namespace Jotly.api.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(UserDto request);
        Task<IEnumerable<UserDto>> GetAllAsync();
    }
}
