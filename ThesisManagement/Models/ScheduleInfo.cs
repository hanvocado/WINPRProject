using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    public class ScheduleInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ThesisId { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        [Required]
        public string EventName { get; set; }

        public string? Location { get; set; }

        public string? Note { get; set; }
        

        [ForeignKey(nameof(ThesisId))]
        public Thesis? Thesis { get; set; }

        public Task? Task { get; set; }
    }
}
