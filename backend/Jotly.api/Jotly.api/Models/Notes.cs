using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jotly.api.Models
{
    public class Notes
    {
        [Key]
        public int NoteId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Created_At { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
