using Jotly.api.DTOs;
using Jotly.api.Models;
using Jotly.api.Repositories.NotesRepo;
using Jotly.api.Repositories.UserRepo;

namespace Jotly.api.Services.UserService
{
    public class UserService (IUserRepository _userRepository) : IUserService
    {

        public async Task<UserDto> CreateAsync(UserDto request) 
        {
            var user = new User()
            {
                Username = request.Username,
                Email = request.Email,
            };

            var entity = await _userRepository.CreateAsync(user);

            var result = new UserDto
            {
                UserId = entity.UserId,
                Username = entity.Username,
                Email = entity.Email
            };

            return result;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync() 
        {
            var entities = await _userRepository.GetAllAsync();

            var results = entities.Select(r => new UserDto
            {
                UserId = r.UserId,
                Email = r.Email,
                Username = r.Username,
            });

            return results;
        }
    }
}
