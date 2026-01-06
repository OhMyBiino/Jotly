using Jotly.api.DTOs;

namespace Jotly.api.Services.NoteService
{
    public interface INoteService
    {
        Task<IEnumerable<NotesDto>> GetAllAsync();
        Task<NotesDto> GetByIdAsync(int id);
        Task AddAsync(NotesDto notesDto);
        Task UpdateAsync(NotesDto notesDto);
        Task DeleteAsync(int Id);
    }
}
