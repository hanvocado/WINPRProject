using System.ComponentModel.DataAnnotations;

namespace ThesisManagement.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string? ProfessorId { get; set; }

        public string? StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [StringLength(100)]
        public string? Technology { get; set; }

        public Professor? Professor { get; set; }
        public ICollection<StudentTopic>? StudentTopics { get; set; }

    }
}
