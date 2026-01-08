using Jotly.api.DTOs;

namespace Jotly.api.Services.UserService
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(CreateUserDto request);
        Task<IEnumerable<UserDto>> GetAllAsync();
    }
}
