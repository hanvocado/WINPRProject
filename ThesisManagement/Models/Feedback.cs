using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ThesisId { get; set; }

        public string Text { get; set; }

        [ForeignKey(nameof(ThesisId))]
        public Thesis Thesis { get; set; }
    }
}
