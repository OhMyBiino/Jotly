using Jotly.api.DTOs;
using Jotly.api.Services.NoteService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jotly.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<NotesDto>>> GetAllNotes() 
        {
            var notes = await _noteService.GetAllAsync();
            return Ok(notes);
        }

        [HttpPost]
        public async Task<ActionResult<CreateNoteDto>> CreateNote([FromBody] CreateNoteDto request) 
        {
            var result = await _noteService.AddAsync(request);

            return Ok(result);
        }
    }
}
