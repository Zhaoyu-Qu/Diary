using Diary.EntityModels;
using Diary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Diary.Api.Controllers
{
    // The line [ApiController] is an attribute that marks the class as an API controller in ASP.NET Core.
    // It enables automatic model validation, binding, and error responses for HTTP APIs. With this attribute, the framework provides helpful features like automatic 400 responses for invalid models and improved parameter binding, making your controller behave as a RESTful API endpoint.
    [ApiController]

    // The [controller] token is automatically replaced with the controller's name (minus "Controller").
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // GET: api/notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetAllNotes()
        {
            var notes = await _noteService.GetAllNotesAsync();
            return Ok(notes);
        }

        // GET: api/notes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNoteById(int id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null)
                return NotFound();
            return Ok(note);
        }

        // POST: api/notes
        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote(Note note)
        {
            var createdNote = await _noteService.CreateNoteAsync(note);
            return CreatedAtAction(nameof(GetNoteById), new { id = createdNote.Id }, createdNote);
        }

        // PUT: api/notes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, Note note)
        {
            if (id != note.Id)
                return BadRequest();
            var updated = await _noteService.UpdateNoteAsync(note);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        // DELETE: api/notes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var deleted = await _noteService.DeleteNoteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
