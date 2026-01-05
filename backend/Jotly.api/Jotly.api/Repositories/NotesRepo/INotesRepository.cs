using Jotly.api.DTOs;
using Jotly.api.Models;

namespace Jotly.api.Repositories.NotesRepo
{
    public interface INotesRepository
    {
        public Task<IEnumerable<Notes>> GetAllAsync();
        Task<Notes> GetByIdAsync(int Id);
        Task CreateAsync(Notes note);
        Task<Notes> UpdateAsync(Notes note);
        Task<bool> DeleteAsync(int Id);
    }
}
