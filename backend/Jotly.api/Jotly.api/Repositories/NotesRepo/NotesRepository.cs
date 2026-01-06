using Jotly.api.Data;
using Jotly.api.DTOs;
using Microsoft.EntityFrameworkCore;
using Jotly.api.Models;
using System.Runtime.InteropServices;

namespace Jotly.api.Repositories.NotesRepo
{
    public class NotesRepository 
        (IDbContextFactory<AppDbContext> _contextFactory) : INotesRepository
    {
        public async Task CreateAsync(Notes note)
        {
            await using var _context = _contextFactory.CreateDbContext();

            _context.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            await using var _context = _contextFactory.CreateDbContext();

            var note = await _context.Notes.FindAsync(Id);
            if (note == null) return false;

            _context.Remove(note);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Notes>> GetAllAsync()
        {
            await using var _context = _contextFactory.CreateDbContext();

            var notes = await _context.Notes
                .AsNoTracking()
                .ToListAsync();

            return notes ?? Enumerable.Empty<Notes>();
        }

        public async Task<Notes> GetByIdAsync(int Id)
        {
            await using var _context = _contextFactory.CreateDbContext();

            var note = await _context.Notes.FindAsync(Id);
            
            return note ?? null!;
        }

        public async Task<Notes> UpdateAsync(Notes note)
        {
            await using var _context = _contextFactory.CreateDbContext();

            var item = await _context.Notes.FindAsync(note.NoteId);
            if (item == null) return item!;

            item.Title = note.Title;
            item.Content = note.Content;

            await _context.SaveChangesAsync();
            return item;
        }
    }
}
