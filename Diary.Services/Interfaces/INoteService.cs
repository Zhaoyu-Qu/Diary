using Diary.EntityModels;

namespace Diary.Services.Interfaces
{
    public interface INoteService
    {
        Task<Note?> GetNoteByIdAsync(int id);
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task<Note> CreateNoteAsync(Note note);
        Task<bool> UpdateNoteAsync(Note note);
        Task<bool> DeleteNoteAsync(int id);
    }
}