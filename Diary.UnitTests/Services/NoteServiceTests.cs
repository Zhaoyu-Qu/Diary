using Diary.EntityModels;
using Diary.Services;
using Diary.Services.Interfaces;
using Diary.UnitTests.TestHelpers;

namespace Diary.UnitTests.Services
{
    public class NoteServiceTests : IDisposable
    {
        private readonly DatabaseFixture _fixture;
        private readonly INoteService _service;

        public NoteServiceTests()
        {
            _fixture = new DatabaseFixture();
            _service = new NoteService(_fixture.Context);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public async Task CreateNoteAsync_ShouldAddNote()
        {
            var note = new Note { Title = "Test", Content = "Content", CreatedDate = DateTime.UtcNow };
            var result = await _service.CreateNoteAsync(note);
            Assert.NotNull(result);
            var retrievedNote = await _service.GetNoteByIdAsync(result.Id);
            Assert.NotNull(retrievedNote);
            Assert.Equal("Test", retrievedNote.Title);
            Assert.Equal("Content", retrievedNote.Content);
        }

        [Fact]
        public async Task GetNoteByIdAsync_ShouldReturnNote()
        {
            var note = new Note { Title = "FindMe", Content = "Content", CreatedDate = DateTime.UtcNow };
            var created = await _service.CreateNoteAsync(note);
            var found = await _service.GetNoteByIdAsync(created.Id);
            Assert.NotNull(found);
            Assert.Equal("FindMe", found.Title);
        }

        [Fact]
        public async Task GetAllNotesAsync_ShouldReturnAllNotes()
        {
            await _service.CreateNoteAsync(new Note { Title = "A", Content = "A", CreatedDate = DateTime.UtcNow });
            await _service.CreateNoteAsync(new Note { Title = "B", Content = "B", CreatedDate = DateTime.UtcNow });
            var notes = await _service.GetAllNotesAsync();
            Assert.True(notes.Count() == 2);
        }

        [Fact]
        public async Task UpdateNoteAsync_ShouldUpdateNote()
        {
            var note = new Note { Title = "Old", Content = "Old", CreatedDate = DateTime.UtcNow };
            var created = await _service.CreateNoteAsync(note);
            created.Title = "New";
            created.Content = "New";
            var updated = await _service.UpdateNoteAsync(created);
            Assert.True(updated);
            var found = await _service.GetNoteByIdAsync(created.Id);
            Assert.NotNull(found);
            Assert.Equal("New", found.Title);
            Assert.Equal("New", found.Content);
        }

        [Fact]
        public async Task DeleteNoteAsync_ShouldRemoveNote()
        {
            var note = new Note { Title = "DeleteMe", Content = "Content", CreatedDate = DateTime.UtcNow };
            var created = await _service.CreateNoteAsync(note);
            Assert.NotNull(created);
            var retrievedNote = await _service.GetNoteByIdAsync(created.Id);
            Assert.NotNull(retrievedNote);
            var deleted = await _service.DeleteNoteAsync(created.Id);
            Assert.True(deleted);
            var found = await _service.GetNoteByIdAsync(created.Id);
            Assert.Null(found);
        }
    }
}