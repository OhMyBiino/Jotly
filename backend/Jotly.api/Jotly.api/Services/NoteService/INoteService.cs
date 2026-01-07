using Jotly.api.DTOs;
using Jotly.api.Models;

namespace Jotly.api.Services.NoteService
{
    public interface INoteService
    {
        Task<IEnumerable<NotesDto>> GetAllAsync();
        Task<NotesDto> GetByIdAsync(int id);
        Task<Notes> AddAsync(CreateNoteDto request);
        Task UpdateAsync(NotesDto notesDto);
        Task DeleteAsync(int Id);
    }
}
