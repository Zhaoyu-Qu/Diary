using Diary.EntityModels;
using Diary.Services.Interfaces;
using Diary.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Diary.Services
{
    public class NoteService : INoteService
    {
        private readonly DiaryDbContext _context;

        public NoteService(DiaryDbContext context)
        {
            _context = context;
        }

        public async Task<Note?> GetNoteByIdAsync(int id)
        {
            return await _context.Notes.FindAsync(id);
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Note> CreateNoteAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<bool> UpdateNoteAsync(Note note)
        {
            var existingNote = await _context.Notes.FindAsync(note.Id);
            if (existingNote == null)
                return false;

            existingNote.Title = note.Title;
            existingNote.Content = note.Content;
            existingNote.CreatedDate = note.CreatedDate;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNoteAsync(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
                return false;

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}