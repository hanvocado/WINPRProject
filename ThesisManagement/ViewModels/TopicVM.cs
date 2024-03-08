using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Windows.Forms;
using System.Windows.Input;
using ThesisManagement.Models;
using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class TopicVM : ViewModelBase
    {
        public string name;
        public string? category;
        public string? technology;
        public string description;

        private readonly ITopicRepository _topicRepo;
        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public TopicsVM topicsVM;


        public IEnumerable<string> Categories { get; set; } = new List<string>() { "Computer Science", "Web Development", "Data Science", "Other" };
        public IEnumerable<string> Technologies { get; set; } = new List<string>() { "JavaScript", "Wpf", ".NET", "Java", "Python", "SQL", "ASP.NET Core", "Other" };
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
                FilterData();
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
                FilterData();
            }
        }

        public string Technology
        {
            get { return technology; }
            set
            {
                technology = value;
                OnPropertyChanged(nameof(Technology));
                FilterData();
            }
        }

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


        private IEnumerable<Topic> filteredTopics;
        public IEnumerable<Topic> FilteredTopics
        {
            get { return filteredTopics; }
            set
            {
                filteredTopics = value;
                OnPropertyChanged(nameof(FilteredTopics));
            }
        }

        public TopicVM()
        {
            topicsVM = new TopicsVM();
            selectedTopic = new Topic();
            _topicRepo = new TopicRepository();
            FilteredTopics = _topicRepo.GetAll();
            CreateCommand = new ViewModelCommand(ExecuteCreateCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            SaveCommand = new ViewModelCommand(ExecuteSaveCommand, CanExecuteSaveCommand);
        }

        private bool CanExecuteDeleteCommand(object obj)
        {
            return true;
        }

        private void ExecuteDeleteCommand(object parameter)
        {
            Topic topic = new Topic
            {
                Id = selectedTopic.Id,
                Name = selectedTopic.Name,
                Category = selectedTopic.Category,
                Technology = selectedTopic.Technology,
                Description = selectedTopic.Description
            };
            //MessageBox.Show($"{topic.Id},{topic.Name}, {topic.Category}, {topic.Technology}, {topic.Description}");
            _topicRepo.Delete(topic.Id);
        }

        private void ExecuteSaveCommand(object obj)
        {
            //topicsVM.Topics = _topicRepo.GetAll();
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

        private void FilterData()
        {
            var filteredData = _topicRepo.GetFilteredTopics(Name, Category, Technology);
            FilteredTopics = new ObservableCollection<Topic>(filteredData);
        }

    }
}
