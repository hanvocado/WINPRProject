using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
        public string? Color { get; set; }

        [ForeignKey(nameof(ThesisId))]
        public Thesis? Thesis { get; set; }

        [NotMapped]
        public Brush ColorBrush { get; }
    }
}
