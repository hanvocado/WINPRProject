using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisManagement.Models
{
    public class TaskProgress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TaskId { get; set; }

        [Required]
        public string? StudentId { get; set; }

        public int Progress { get; set; }

        public string? Description { get; set; }

        public string? Response { get; set; }

        public DateTime UpdateAt { get; set; }

        [ForeignKey(nameof(TaskId))]
        public Task Task { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }

        public ICollection<Attachement>? Attachements { get; set; }

    }
}
