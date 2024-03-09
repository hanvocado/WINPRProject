using System.ComponentModel.DataAnnotations;

namespace ThesisManagement.Models
{
    public class StudentTopic
    {
        [Required]
        [StringLength(20)]
        public string StudentId { get; set; }
        [Required]
        public int TopicId { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        public Student Student { get; set; }
        public Topic Topic { get; set; }

    }
}
