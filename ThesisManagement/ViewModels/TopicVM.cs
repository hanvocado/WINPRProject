using System.ComponentModel;
using ThesisManagement.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ThesisManagement.ViewModels
{
    public class TopicVM : ViewModelBase
    {
        public string? name;
        public string? category;
        public string? technology;
        public string? description;

        public string? Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string? Category
        {
            get => category;
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public string? Technology
        {
            get => technology;
            set
            {
                technology = value;
                OnPropertyChanged(nameof(Technology));
            }
        }

        public string? Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public IEnumerable<string> Categories { get; set; } = new List<string>() { "Education", "Health", "Business", "Other" };
        public IEnumerable<string> Technologies { get; set; } = new List<string>() { "JavaScript", "Wpf", ".NET", "Java", "Python", "Other" };

        public Topic selectedTopic()
        {
            return new Topic
            {
                ProfessorId = "Prof000",
                StudentId = "Stud000",
                Name = this.Name,
                Category = this.Category,
                Technology = this.Technology,
                Description = this.Description
            };
        }
    }
}
