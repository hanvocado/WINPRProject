using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisManagement.Models
{
    public class StudentTopic
    {
        [Required]
        [StringLength(20)]
        public string StudentId { get; set; }
        [Required]
        public int TopicId { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public Student? Student { get; set; }
        public Topic? Topic { get; set; }

    }
}
