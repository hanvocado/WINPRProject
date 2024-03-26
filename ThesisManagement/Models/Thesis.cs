using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThesisManagement.Helpers;

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

        public byte[]? File { get; set; }

        public float? Score { get; set; }

        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }

        public ICollection<Feedback>? Feedbacks { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<Task>? Tasks { get; set; }


        [NotMapped]
        public string StatusColor
        {
            get
            {
                if (TopicStatus == Variable.StatusTopic.Waiting)
                    return "Yellow";
                else if (TopicStatus == Variable.StatusTopic.Approved)
                    return "Green";
                else
                    return "Red";
            }
        }

        [NotMapped]
        public string StatusHoverColor
        {
            get
            {
                if (TopicStatus == Variable.StatusTopic.Waiting)
                    return "Yellow";
                else if (TopicStatus == Variable.StatusTopic.Approved)
                    return "Green";
                else
                    return "Red";
            }
        }
    }
}
