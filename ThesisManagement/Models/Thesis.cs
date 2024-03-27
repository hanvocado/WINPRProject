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

        public byte[]? File { get; set; }

        public float? Score { get; set; }

        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }

        public ICollection<Feedback>? Feedbacks { get; set; }

        public ICollection<Student>? Students { get; set; }

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        [NotMapped]
        public int NoUnreadNotifications
        {
            get
            {
                return Notifications.Where(n => n.Read == false).Count();
            }
        }
    }
}
