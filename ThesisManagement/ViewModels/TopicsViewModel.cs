using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ProfessorTopicsView = ThesisManagement.Views.Professor.TopicsView;
using ProfessorTopicView = ThesisManagement.Views.Professor.TopicView;
using StudentTopicsView = ThesisManagement.Views.Student.TopicsView;
using StudentTopicView = ThesisManagement.Views.Student.TopicView;

namespace ThesisManagement.ViewModels
{
    public class TopicsViewModel : ViewModelBase
    {
        private readonly ITopicRepository _topicRepo;
        private readonly IProfessorRepository _professorRepo;

        private ObservableCollection<Topic> topics;

        private ObservableCollection<Professor> professors;

        private Topic selectedTopic;

        public string name;
        public string? category;
        public string? technology;
        public string description;
        public string professorName;

        public IEnumerable<string> Categories { get; set; } = new List<string>() { "Computer Science", "Web Development", "Data Science", "Other" };
        public IEnumerable<string> Technologies { get; set; } = new List<string>() { "JavaScript", "Wpf", ".NET", "Java", "Python", "SQL", "ASP.NET Core", "Other" };
        public ObservableCollection<string> ProfessorNames { get; set; }
        public ICommand ProfessorCreateTopic { get; set; }
        public ICommand StudentCreateTopic { get; set; }
        public ICommand CreateOrUpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }

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

        public ObservableCollection<Topic> Topics
        {
            get { return topics; }
            set
            {
                topics = value;
                OnPropertyChanged(nameof(Topics));
            }
        }

        public ObservableCollection<Professor> Professors
        {
            get { return professors; }
            set
            {
                professors = value;
                OnPropertyChanged(nameof(Professors));
            }
        }

        public string ProfessorName
        {
            get { return professorName; }
            set
            {
                professorName = value;
                OnPropertyChanged(nameof(ProfessorName));
                FilterData();
            }
        }

        public TopicsViewModel()
        {
            selectedTopic = new Topic();
            _topicRepo = new TopicRepository();
            _professorRepo = new ProfessorRepository();
            Topics = _topicRepo.GetAll();
            ProfessorNames = _professorRepo.GetProfessorNames();
            ProfessorCreateTopic = new ViewModelCommand(ExecuteProfessorCreateCommand);
            StudentCreateTopic = new ViewModelCommand(ExecuteStudentCreateCommand);
            CreateOrUpdateCommand = new ViewModelCommand(ExecuteCreateOrUpdateCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand);
        }

        private void ExecuteProfessorCreateCommand(object sender)
        {
            var vm = new TopicsViewModel();
            ProfessorTopicsView topicsView = sender as ProfessorTopicsView;
            if (topicsView != null)
            {
                topicsView.DataContext = vm;
            }
            ProfessorTopicView topicView = new();
            topicView.DataContext = vm;
            topicView.Owner = Application.Current.MainWindow;
            topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            topicView.Show();
        }

        private void ExecuteStudentCreateCommand(object sender)
        {
            var vm = new TopicsViewModel();
            StudentTopicsView topicsView = sender as StudentTopicsView;
            if (topicsView != null)
            {
                topicsView.DataContext = vm;
            }
            StudentTopicView topicView = new();
            topicView.DataContext = vm;
            topicView.Owner = Application.Current.MainWindow;
            topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            topicView.Show();
        }

        private void ExecuteCreateOrUpdateCommand(object obj)
        {
            ProfessorTopicView topicView = obj as ProfessorTopicView;
            Topic topic = new Topic
            {
                Id = selectedTopic.Id,
                ProfessorId = selectedTopic.ProfessorId,
                StudentId = selectedTopic.StudentId,
                Name = selectedTopic.Name,
                Category = selectedTopic.Category,
                Technology = selectedTopic.Technology,
                Description = selectedTopic.Description
            };

            if (topic.Id == 0)
            {
                _topicRepo.Add(topic);
            }
            else
            {
                _topicRepo.Update(topic);
            }

            if (topicView != null)
            {
                topicView.Close();
            }

            var mainWindow = Application.Current.MainWindow;
            mainWindow.Focus();

            Topics = _topicRepo.GetAll();
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
            _topicRepo.Delete(topic.Id);
            Topics = _topicRepo.GetAll();
        }

        private void FilterData()
        {
            var filteredData = _professorRepo.GetFilteredTopics(Category, Technology, ProfessorName);
            Topics = new ObservableCollection<Topic>(filteredData);
        }
    }
}
