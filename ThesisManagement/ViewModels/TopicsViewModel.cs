using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ProfessorTopicView = ThesisManagement.Views.Professor.TopicView;
//using StudentTopicsView = ThesisManagement.Views.Student.TopicsView;
using StudentTopicView = ThesisManagement.Views.Student.TopicView;

namespace ThesisManagement.ViewModels
{
    public class TopicsViewModel : ViewModelBase
    {
        private readonly ITopicRepository _topicRepo;
        private readonly IProfessorRepository _professorRepo;
        private readonly IStudentRepository _studentRepo;

        private readonly string currentUserId;

        private ObservableCollection<Topic> topics;

        private ObservableCollection<Professor> professors;
        private ObservableCollection<Student> students;

        private Topic selectedTopic;

        private string? filterName;
        private string? filterCategory;
        private string? filterTechnology;
        private string? filterProfessorName;
        private string studentFilter;

        private

        public IEnumerable<string> Categories
        { get; set; } = new List<string>() { "Computer Science", "Web Development", "Data Science", "Other" };
        public IEnumerable<string> Technologies { get; set; } = new List<string>() { "JavaScript", "Wpf", ".NET", "Java", "Python", "SQL", "ASP.NET Core", "Other" };
        public ICommand ProfessorCreateTopic { get; set; }
        public ICommand StudentCreateTopic { get; set; }
        public ICommand CreateOrUpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public string CurrentUserId
        {
            get { return currentUserId; }
        }

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

        public string FilterName
        {
            get { return filterName; }
            set
            {
                filterName = value;
                OnPropertyChanged(nameof(FilterName));
                FilterData();
            }
        }

        public string FilterCategory
        {
            get { return filterCategory; }
            set
            {
                filterCategory = value;
                OnPropertyChanged(nameof(FilterCategory));
                FilterData();
            }
        }

        public string FilterTechnology
        {
            get { return filterTechnology; }
            set
            {
                filterTechnology = value;
                OnPropertyChanged(nameof(FilterTechnology));
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

        public ObservableCollection<Student> Students
        {
            get { return students; }
            set
            {
                students = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        public string FilterProfessorName
        {
            get { return filterProfessorName; }
            set
            {
                filterProfessorName = value;
                OnPropertyChanged(nameof(FilterProfessorName));
                FilterData();
            }
        }

        public string StudentFilter
        {
            get { return studentFilter; }
            set
            {
                studentFilter = value;
                OnPropertyChanged(nameof(StudentFilter));
                FilterStudent();
            }
        }

        public TopicsViewModel()
        {
            currentUserId = SessionInfo.UserId;
            selectedTopic = new Topic();
            studentFilter = "";
            _topicRepo = new TopicRepository();
            _professorRepo = new ProfessorRepository();
            _studentRepo = new StudentRepository();
            Topics = _topicRepo.GetAll();
            Students = _studentRepo.GetAll();
            Professors = _professorRepo.GetAll();
            ProfessorCreateTopic = new ViewModelCommand(ExecuteProfessorCreateCommand);
            StudentCreateTopic = new ViewModelCommand(ExecuteStudentCreateCommand);
            CreateOrUpdateCommand = new ViewModelCommand(ExecuteCreateOrUpdateCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand);
        }

        private void ExecuteProfessorCreateCommand(object sender)
        {
            this.SelectedTopic = new Topic();
            ProfessorTopicView topicView = new();
            topicView.DataContext = this;
            topicView.Owner = Application.Current.MainWindow;
            topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            topicView.Show();
        }

        private void ExecuteStudentCreateCommand(object sender)
        {
            this.SelectedTopic = new Topic();
            StudentTopicView topicView = new();
            topicView.DataContext = this;
            topicView.Owner = Application.Current.MainWindow;
            topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            topicView.Show();
        }

        private void ExecuteCreateOrUpdateCommand(object obj)
        {
            ProfessorTopicView topicView = obj as ProfessorTopicView;
            selectedTopic.ProfessorId = selectedTopic.ProfessorId ?? currentUserId;
            if (selectedTopic.Id <= 0)
            {
                _topicRepo.Add(selectedTopic);
            }
            else
            {
                _topicRepo.Update(selectedTopic);
            }

            Topics = _topicRepo.GetAll();
            if (topicView != null)
            {
                topicView.Close();
            }

            var mainWindow = Application.Current.MainWindow;
            mainWindow.Focus();

        }

        private void ExecuteDeleteCommand(object parameter)
        {
            _topicRepo.Delete(selectedTopic.Id);
            Topics = _topicRepo.GetAll();
        }

        private void FilterData()
        {
            Topics = _topicRepo.GetFilteredTopics(Category, Technology, ProfessorName);
        }

        private void FilterStudent()
        {
            Students = _studentRepo.Get(studentFilter);
            MessageBox.Show(Students.ToList().Count.ToString());
        }
    }
}
