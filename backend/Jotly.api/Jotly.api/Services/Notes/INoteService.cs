using Jotly.api.DTOs;

namespace Jotly.api.Services.Notes
{
    public interface INoteService
    {
        Task<IEnumerable<NotesDto>> GetAllAsync();
        Task<NotesDto> GetByIdAsync(int id);
        Task AddAsync(NotesDto notesDto);
        Task<NotesDto> UpdateAsync(NotesDto notesDto);
        Task<NotesDto> DeleteAsync(int Id);
    }
}
