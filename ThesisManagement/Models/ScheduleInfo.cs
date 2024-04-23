using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

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
        public string? Color { get; set; }

        [ForeignKey(nameof(ThesisId))]
        public Thesis? Thesis { get; set; }

        [NotMapped]
        public Brush? Background { get; set; }

        [NotMapped]
        public string DisplayFrom
        {
            get
            {
                return From.ToString("HH:mm dd-MM-yyyy");
            }
        }

        [NotMapped]
        public string DisplayTo
        {
            get
            {
                return To.ToString("HH:mm dd-MM-yyyy");
            }
        }
    }
}
