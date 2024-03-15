using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThesisManagement.Helpers;

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

        //[Required]
        public string? Requirement { get; set; }

        public int StudentQuantity { get; set; } = 1;

        [StringLength(100)]
        public string? Technology { get; set; }

        public Professor Professor { get; set; }
        public ICollection<Thesis>? Theses { get; set; }
        public ICollection<Task>? Tasks { get; set; }


        [NotMapped]
        public string PenColor
        {
            get
            {
                if (Theses != null)
                {
                    foreach (Thesis thesis in Theses)
                    {
                        if (thesis.TopicStatus == Variable.StatusTopic.Waiting)
                        {
                            return "#ffdd52";
                        }
                    }
                }
                return "LightGray";
            }
        }

        [NotMapped]
        public bool PenEnable
        {
            get
            {
                return PenColor != "LightGray";
            }
        }

    }
}
