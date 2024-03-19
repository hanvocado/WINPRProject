using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
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

        private Topic? selectedTopic;

        public Topic? SelectedTopic
        {
            get { return selectedTopic; }
            set { selectedTopic = value; OnPropertyChanged(nameof(SelectedTopic)); }
        }


        private ObservableCollection<Topic> topics;

        private ObservableCollection<Professor> professors;
        private ObservableCollection<Student> students;

        private string? filterName;
        private string? filterCategory;
        private string? filterTechnology;
        private string? filterProfessorName;
        private string studentFilter;

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string? professorId;

        [StringLength(20)]
        [Required(ErrorMessage = "Giảng viên không thể để trống")]
        public string ProfessorId
        {
            get { return professorId; }
            set
            {
                professorId = value;
                OnPropertyChanged(nameof(ProfessorId));
            }
        }

        private string? studentId;
        public string? StudentId
        {
            get { return studentId; }
            set
            {
                studentId = value;
                OnPropertyChanged(nameof(StudentId));
            }
        }

        private string? name;

        [Required(ErrorMessage = "Tên đề tài không thể để trống")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string? category;

        [Required(ErrorMessage = "Thể loại không thể để trống")]
        [StringLength(100)]
        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        private string? technology;

        [StringLength(100)]
        [Required(ErrorMessage = "Thể loại không thể để trống")]
        public string Technology
        {
            get { return technology; }
            set
            {
                technology = value;
                OnPropertyChanged(nameof(Technology));
            }
        }

        private string? description;
        public string? Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(nameof(Description)); }
        }

        private string? requirement;
        public string? Requirement
        {
            get { return requirement; }
            set { requirement = value; OnPropertyChanged(nameof(Requirement)); }
        }

        private int studentQuantity;

        [Range(1, 5)]
        public int StudentQuantity
        {
            get { return studentQuantity; }
            set
            {
                studentQuantity = value;
                OnPropertyChanged(nameof(StudentQuantity));
            }
        }


        public IEnumerable<string> Categories
        { get; set; } = new List<string>() { "Computer Science", "Web Development", "Data Science", "Other" };
        public IEnumerable<string> Technologies { get; set; } = new List<string>() { "JavaScript", "Wpf", ".NET", "Java", "Python", "SQL", "ASP.NET Core", "Other" };
        public ViewModelCommand ProfessorCreateTopic { get; set; }
        public ViewModelCommand StudentCreateTopic { get; set; }
        public ViewModelCommand CreateOrUpdateCommand { get; set; }
        public ViewModelCommand DeleteCommand { get; set; }
        public ViewModelCommand SaveCommand { get; set; }

        public string CurrentUserId
        {
            get { return currentUserId; }
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
            studentFilter = "";
            _topicRepo = new TopicRepository();
            _professorRepo = new ProfessorRepository();
            _studentRepo = new StudentRepository();
            Topics = _topicRepo.GetAll();
            Students = _studentRepo.GetAll();
            Professors = _professorRepo.GetAll();
            ProfessorCreateTopic = new ViewModelCommand(ExecuteProfessorCreateCommand);
            StudentCreateTopic = new ViewModelCommand(ExecuteStudentCreateCommand);
            CreateOrUpdateCommand = new ViewModelCommand(ExecuteCreateOrUpdateCommand, CanCreateOrUpdateTopic);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand);
        }

        private bool CanCreateOrUpdateTopic(object obj)
        {
            return true;
        }

        private void ExecuteProfessorCreateCommand(object sender)
        {
            ProfessorTopicView topicView = new();
            ResetTopicProperties();
            this.ProfessorId = currentUserId;
            topicView.DataContext = this;
            topicView.Owner = Application.Current.MainWindow;
            topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            topicView.Show();
        }

        private void ExecuteStudentCreateCommand(object sender)
        {
            StudentTopicView topicView = new();
            //this.StudentId = currentUserId;
            topicView.DataContext = this;
            topicView.Owner = Application.Current.MainWindow;
            topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            topicView.Show();
        }

        private void ExecuteCreateOrUpdateCommand(object obj)
        {
            if (IsTopicNotValid())
                return;

            ProfessorTopicView topicView = obj as ProfessorTopicView;
            Topic topic = new Topic
            {
                Id = id,
                Name = name,
                ProfessorId = professorId,
                StudentId = studentId,
                Category = category,
                Technology = technology,
                Description = description,
                Requirement = requirement,
                StudentQuantity = studentQuantity
            };

            if (id <= 0)
                _topicRepo.Add(topic);
            else
                _topicRepo.Update(topic);

            Topics = _topicRepo.GetAll();

            if (topicView != null)
                topicView.Close();

            var mainWindow = Application.Current.MainWindow;
            mainWindow.Focus();
        }

        private void ExecuteDeleteCommand(object parameter)
        {
            _topicRepo.Delete(id);
            Topics = _topicRepo.GetAll();
        }

        private void FilterData()
        {
            Topics = _topicRepo.GetFilteredTopics(FilterCategory, FilterTechnology, FilterProfessorName);
        }

        private void FilterStudent()
        {
            Students = _studentRepo.Get(studentFilter);
            MessageBox.Show(Students.ToList().Count.ToString());
        }

        private void ResetTopicProperties()
        {
            Id = 0;
            Name = "";
            Description = "";
            Requirement = "";
            Category = "";
            Technology = "";
            studentQuantity = 1;
        }

        private bool IsTopicNotValid()
        {
            bool isProfessorValid = Validate(nameof(ProfessorId), professorId, CreateOrUpdateCommand);
            bool isNameValid = Validate(nameof(Name), name, CreateOrUpdateCommand);
            bool isCategoryValid = Validate(nameof(Category), category, CreateOrUpdateCommand);
            bool isTechnologyValid = Validate(nameof(Technology), technology, CreateOrUpdateCommand);
            bool isStudentQuantityValid = Validate(nameof(StudentQuantity), studentQuantity, CreateOrUpdateCommand);
            return !isProfessorValid || !isNameValid || !isCategoryValid || !isTechnologyValid || !isStudentQuantityValid;
        }
    }
}
