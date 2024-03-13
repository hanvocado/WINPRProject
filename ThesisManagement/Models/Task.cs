using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ThesisId { get; set; }

        public string Text { get; set; }

        public DateTime DeadLine { get; set; }

        public bool Status { get; set; }

        [ForeignKey(nameof(ThesisId))]
        public Thesis Thesis { get; set; }
    }
}
