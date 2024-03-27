using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public int ThesisId { get; set; }

        public bool Read { get; set; } = false;


        [ForeignKey(nameof(ThesisId))]
        public Thesis Thesis { get; set; }
    }
}
