using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string? Requirement { get; set; }
        public string? Function { get; set; }

        public int StudentQuantity { get; set; }

        [StringLength(100)]
        public string? Technology { get; set; }

        public Professor Professor { get; set; }
        public ICollection<Thesis>? Theses { get; set; }

        [NotMapped]
        public string? RegisteredStatusByCurrentUser { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Technology
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; } = false;

        public Technology(string name)
        {
            Name = name;
        }

        public Technology(string name, bool isSelected)
        {
            Name = name;
            IsSelected = isSelected;
        }

        public static List<Technology> GetTechnologies()
        {
            return new List<Technology>
                    {   new Technology("MernStack"),
                        new Technology("WPF"),
                        new Technology(".NET"),
                        new Technology("Java"),
                        new Technology("Python"),
                        new Technology("Other")
                    };
        }
    }

    public class Category
    {
        public static List<string> GetCategories()
        {
            return new List<string>() { "Desktop App", "Web App", "Data Science", "Machine Learning", "Other" };
        }
    }
}
