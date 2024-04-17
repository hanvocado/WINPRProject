using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    public class TaskProgress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TaskId { get; set; }

        public string? StudentId { get; set; }

        public int Progress { get; set; }

        public string? Description { get; set; }

        public string? Response { get; set; }

        public DateTime? StudentUpdateAt { get; set; }
        public DateTime? ProfessorUpdateAt { get; set; }

        [ForeignKey(nameof(TaskId))]
        public Task? Task { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }

        public ICollection<Attachment>? Attachments { get; set; }

    }
}
