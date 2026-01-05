namespace Jotly.api.DTOs
{
    public class NotesDto
    {
        public int NoteId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Created_At { get; set; } 
    }
}
