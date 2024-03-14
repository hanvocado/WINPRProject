using System.ComponentModel.DataAnnotations;

namespace ThesisManagement.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string ProfessorId { get; set; }

        public string? StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        public string Requirement { get; set; }

        public int StudentQuantity { get; set; }

        [StringLength(100)]
        public string? Technology { get; set; }

        public Professor Professor { get; set; }
        public ICollection<Thesis>? Theses { get; set; }
        public ICollection<Task>? Tasks { get; set; }


    }
}
