using System.Collections.ObjectModel;
using System.Windows.Input;
using ThesisManagement.Models;
using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class TopicVM : ViewModelBase
    {
        public string Name { get; set; }
        public string? Category { get; set; }
        public string? Technology { get; set; }
        public string Description { get; set; }

        private readonly ITopicRepository _topicRepo;
        public ICommand CreateCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public TopicsVM topicsVM;


        public IEnumerable<string> Categories { get; set; } = new List<string>() { "Education", "Health", "Business", "Other" };
        public IEnumerable<string> Technologies { get; set; } = new List<string>() { "JavaScript", "Wpf", ".NET", "Java", "Python", "Other" };

        public Topic selectedTopic;
        public Topic SelectedTopic
        {
            get
            {
                return selectedTopic;
            }
            set
            {
                selectedTopic = value;
                OnPropertyChanged(nameof(SelectedTopic));
            }
        }
        public TopicVM()
        {
            topicsVM = new TopicsVM();
            selectedTopic = new Topic();
            _topicRepo = new TopicRepository();
            CreateCommand = new ViewModelCommand(ExecuteCreateCommand);
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
        }

        private void ExecuteSaveCommand(object obj)
        {
            topicsVM.Topics = _topicRepo.GetAll();
        }

        private bool CanExecuteSaveCommand(object obj)
        {
            return true;
        }

        private void ExecuteCreateCommand(object obj)
        {
            Topic newTopic = new Topic
            {
                Name = selectedTopic.Name,
                Category = selectedTopic.Category,
                Technology = selectedTopic.Technology,
                Description = selectedTopic.Description
            };

            _topicRepo.Add(newTopic);
        }

    }
}
