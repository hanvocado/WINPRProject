using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ThesisManagement.Views.Student.DetailFeedback;

namespace ThesisManagement.Models
{
    public class Attachement
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
