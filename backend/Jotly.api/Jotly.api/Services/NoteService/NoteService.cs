using Jotly.api.DTOs;
using Jotly.api.Models;
using Jotly.api.Repositories.NotesRepo;

namespace Jotly.api.Services.NoteService
{
    public class NoteService (INotesRepository _noteRepository) : INoteService
    {

        public async Task<IEnumerable<NotesDto>> GetAllAsync() 
        {
            try
            {
                var results = await _noteRepository.GetAllAsync();

                var notes = results.Select(n => 
                      new NotesDto() 
                      {
                        NoteId = n.NoteId,
                        Title = n.Title,
                        Content = n.Content,
                        Created_At = n.Created_At,
                        UserId = n.UserId,
                      }
                );

                return notes;
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public async Task<NotesDto> GetByIdAsync(int id) 
        {
            try
            {
                var result = await _noteRepository.GetByIdAsync(id);

                var note = new NotesDto()
                {
                    NoteId = result.NoteId,
                    Title = result.Title,
                    Content = result.Content,
                    Created_At = result.Created_At,
                    UserId = result.UserId
                };

                return note;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task<Notes> AddAsync(CreateNoteDto request) 
        {
            try
            {
                var note = new Notes()
                {
                    Title = request.Title,
                    Content = request.Content,
                    Created_At = DateTime.UtcNow,
                    UserId = request.UserId,
                };

                return await _noteRepository.CreateAsync(note);
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public async Task UpdateAsync(NotesDto notesDto) 
        {
            try
            {
                var note = new Notes()
                {
                    NoteId = notesDto.NoteId,
                    Title = notesDto.Title,
                    Content = notesDto.Content,
                };

                await _noteRepository.UpdateAsync(note);
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public async Task DeleteAsync(int Id) 
        {
            try
            {
                await _noteRepository.DeleteAsync(Id);
            }
            catch (Exception) 
            {
                throw;
            }
        }

    }
}
