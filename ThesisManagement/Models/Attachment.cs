using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TaskProgressId { get; set; }

        public string? AttachedFile { get; set; }

        [ForeignKey(nameof(TaskProgressId))]
        public TaskProgress TaskProgress { get; set; }
    }
}
