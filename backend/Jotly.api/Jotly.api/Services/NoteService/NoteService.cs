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
                      }
                );

                return notes;
            }
            catch (Exception ex) 
            {
                throw new Exception($"An error occured while getting all Notes, {ex.Message}");
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
                };

                return note;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured why retrieving the note, {ex.Message}");
            }            
        }

        public async Task AddAsync(NotesDto notesDto) 
        {
            try
            {
                var note = new Notes()
                {
                    Title = notesDto.Title,
                    Content = notesDto.Content
                };

                await _noteRepository.CreateAsync(note);
            }
            catch (Exception ex) 
            {
                throw new Exception($" An error occured while adding note, {ex.Message}");
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
            catch (Exception ex) 
            {
                throw new Exception($"An error occured while updating the Note, {ex.Message}");
            }
        }

        public async Task DeleteAsync(int Id) 
        {
            try
            {
                await _noteRepository.DeleteAsync(Id);
            }
            catch (Exception ex) 
            {
                throw new Exception($"An error occure while deleting the note, {ex.Message}");
            }
        }

    }
}
