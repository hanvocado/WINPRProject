using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    public class Thesis
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TopicId { get; set; }

        [Required]
        [StringLength(50)]
        public string TopicStatus { get; set; }

        public string? File { get; set; }

        public float? Score { get; set; }

        public string? Evaluation { get; set; }

        public int? NoUpdates { get; set; }

        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }

        public ICollection<Feedback>? Feedbacks { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<Task>? Tasks { get; set; }
        public ICollection<ScheduleInfo>? ScheduleInfos { get; set; }

        public int WaitingForResponse { get; set; }
    }
}
