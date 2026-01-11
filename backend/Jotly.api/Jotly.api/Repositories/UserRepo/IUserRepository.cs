
using Jotly.api.Models;

namespace Jotly.api.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int Id);
        Task<User?> GetByUsernameAsync(string username);
    }
}
