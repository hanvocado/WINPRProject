using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    public class Thesis
    {
        [Key]
        public int Id { get; set; }

        public int? TopicId { get; set; }

        [Required]
        [StringLength(50)]
        public string TopicStatus { get; set; }

        public byte[]? File { get; set; }

        public float? Score { get; set; }

        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }

        public Task? Task { get; set; }

        public ICollection<Feedback>? Feedbacks { get; set; }
        
        public ICollection<Student>? Students { get; set; }
    }
}
