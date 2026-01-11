using Jotly.api.Data;
using Jotly.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Jotly.api.Repositories.UserRepo
{
    public class UserRepository(IDbContextFactory<AppDbContext> _contextFactory) : IUserRepository
    {
        public async Task<User> CreateAsync(User user) 
        {
            await using var _context = _contextFactory.CreateDbContext();

            var newUser = await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return newUser.Entity;
        }

        public async Task<IEnumerable<User>> GetAllAsync() 
        {
            await using var _context = _contextFactory.CreateDbContext();

            var users = await _context.Users.ToListAsync();

            return users ?? Enumerable.Empty<User>();
        }

        public async Task<User?> GetByIdAsync(int Id) 
        {
            await using var _context = _contextFactory.CreateDbContext();

            return await _context.Users.FindAsync(Id);
        }

        public async Task<User?> GetByUsernameAsync(string username) 
        {
            await using var _context = _contextFactory.CreateDbContext();
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username.ToLower());
        }
    }
}
