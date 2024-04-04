using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Student;
using ProfessorTopicView = ThesisManagement.Views.Professor.TopicView;
using StudentTopicView = ThesisManagement.Views.Student.TopicView;

namespace ThesisManagement.ViewModels
{
    public class TopicsViewModel : ViewModelBase
    {
        private readonly ITopicRepository _topicRepo;
        private readonly IProfessorRepository _professorRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IThesisRepository _thesisRepo;
        private User currentUser;

        private Topic selectedTopic;
        public Topic SelectedTopic
        {
            get { return selectedTopic; }
            set { selectedTopic = value; OnPropertyChanged(nameof(SelectedTopic)); }
        }

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

        [Required(ErrorMessage = "Mô tả không thể để trống")]
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

        private string? function;

        [Required(ErrorMessage = "Chức năng không thể để trống")]
        public string? Function
        {
            get { return function; }
            set { function = value; OnPropertyChanged(nameof(Function)); }
        }


        private int studentQuantity;

        [Range(1, 5, ErrorMessage = "Số lượng từ 1 đến 5")]
        public int StudentQuantity
        {
            get { return studentQuantity; }
            set
            {
                studentQuantity = value;
                OnPropertyChanged(nameof(StudentQuantity));
            }
        }

        private string? filterTopicName;
        public string? FilterTopicName
        {
            get { return filterTopicName; }
            set
            {
                filterTopicName = value;
                OnPropertyChanged(nameof(FilterTopicName));
                Topics = _topicRepo.GetByTopicName(value);
            }
        }
        private string? filterProfessorName;
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

        private string? filterCategory;
        public string? FilterCategory
        {
            get { return filterCategory; }
            set
            {
                filterCategory = value;
                OnPropertyChanged(nameof(FilterCategory));
                FilterData();
            }
        }

        private string? filterTechnology;
        public string? FilterTechnology
        {
            get { return filterTechnology; }
            set
            {
                filterTechnology = value;
                OnPropertyChanged(nameof(FilterTechnology));
                FilterData();
            }
        }

        private string filterStudent;
        public string StudentFilter
        {
            get { return filterStudent; }
            set
            {
                filterStudent = value;
                OnPropertyChanged(nameof(StudentFilter));
                FilterStudent();
            }
        }

        private string selectedStudentNames;
        public string SelectedStudentNames
        {
            get { return selectedStudentNames; }
            set { selectedStudentNames = value; OnPropertyChanged(nameof(SelectedStudentNames)); }
        }

        public IEnumerable<string> Categories { get; set; } = new List<string>() { "Desktop App", "Web App", "Data Science", "Machine Learning", "Other" };
        public IEnumerable<string> Technologies { get; set; } = new List<string>() { "MernStack", "Wpf", ".NET", "Java", "Python", "Other" };

        private List<Topic> topics;
        public List<Topic> Topics
        {
            get { return topics; }
            set
            {
                topics = value;
                OnPropertyChanged(nameof(Topics));
            }
        }

        private ObservableCollection<Professor> professors;
        public ObservableCollection<Professor> Professors
        {
            get { return professors; }
            set
            {
                professors = value;
                OnPropertyChanged(nameof(Professors));
            }
        }

        private List<Student> selectedStudents;
        public List<Student> SelectedStudents
        {
            get { return selectedStudents; }
            set
            {
                selectedStudents = value;
                OnPropertyChanged(nameof(SelectedStudents));
            }
        }

        private List<Student> students;
        public List<Student> Students
        {
            get { return students; }
            set
            {
                students = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        public ViewModelCommand ShowTopicViewCommand { get; set; }
        public ViewModelCommand CreateOrUpdateCommand { get; set; }
        public ViewModelCommand DeleteCommand { get; set; }
        public ViewModelCommand SaveCommand { get; set; }
        public ViewModelCommand RegisterTopicCommand { get; set; }
        public ViewModelCommand RegisterNewTopicCommand { get; set; }
        public ViewModelCommand ChooseMembersCommand { get; set; }
        public ViewModelCommand AddMembersCommand { get; set; }

        public TopicsViewModel()
        {
            _topicRepo = new TopicRepository();
            _professorRepo = new ProfessorRepository();
            _studentRepo = new StudentRepository();
            _thesisRepo = new ThesisRepository();
            SelectedStudents = new List<Student>();
            SelectedTopic = new Topic();

            if (SessionInfo.Role == Role.Professor)
                Topics = _topicRepo.GetAll(SessionInfo.UserId);
            else
                Topics = _topicRepo.GetMyTopicAndProfessorTopics(SessionInfo.UserId);

            Students = _studentRepo.GetUnRegisteredStudents();
            Professors = _professorRepo.GetAll();
            ShowTopicViewCommand = new ViewModelCommand(ExecuteShowTopicView, CanShowTopicView);
            CreateOrUpdateCommand = new ViewModelCommand(ExecuteCreateOrUpdateCommand, CanCreateOrUpdateTopic);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDelete);
            RegisterTopicCommand = new ViewModelCommand(ExecuteRegisterTopicCommand, CanStudentRegisterTopic);
            RegisterNewTopicCommand = new ViewModelCommand(ExecuteRegisterNewTopicCommand, CanStudentRegisterTopic);
            ChooseMembersCommand = new ViewModelCommand(ExecuteChooseMembers, CanStudentRegisterTopic);
            AddMembersCommand = new ViewModelCommand(ExecuteAddMembers);
        }

        private bool CanStudentRegisterTopic(object obj)
        {
            return _studentRepo.CanRegisterTopic(SessionInfo.UserId);
        }

        private void ExecuteAddMembers(object obj)
        {
            var members = Students.Where(s => s.IsSelected).ToList();
            SelectedStudents.Clear();
            var currentStudent = _studentRepo.GetStudent(SessionInfo.UserId);
            SelectedStudents.Add(currentStudent);
            SelectedStudentNames = SessionInfo.Name;
            foreach (var st in members)
            {
                SelectedStudentNames += $" - {st.Name}";
                SelectedStudents.Add(st);
            }
            if (selectedTopic?.StudentQuantity > 0 && selectedStudents.Count > selectedTopic?.StudentQuantity)
            {
                ShowMessage(false, "", Message.ExceedStudentQuantity);
                return;
            }
            var chooseMembersView = obj as ChooseMembersView;
            chooseMembersView?.Close();
        }

        private void ExecuteChooseMembers(object obj)
        {
            var popup = new ChooseMembersView();
            popup.DataContext = this;
            popup.Show();
        }

        private void ExecuteRegisterNewTopicCommand(object obj)
        {
            if (IsTopicNotValid())
                return;

            UpdateSelectedTopicProperties();
            bool isTopicCreated = _topicRepo.Add(selectedTopic);

            if (isTopicCreated)
            {
                RegisterTopic(obj);
            }
        }

        private void ExecuteRegisterTopicCommand(object obj)
        {
            if (selectedStudents.Count > selectedTopic?.StudentQuantity)
            {
                ShowMessage(false, "", Message.ExceedStudentQuantity);
                return;
            }
            RegisterTopic(obj);
        }

        private void RegisterTopic(object obj)
        {
            Window? currentView = obj as Window;
            Thesis thesis = new Thesis
            {
                TopicId = SelectedTopic.Id,
                TopicStatus = Variable.StatusTopic.Waiting,
            };
            var success = _thesisRepo.Add(thesis, selectedStudents);
            ShowMessage(success, Message.RegisterSuccess, Message.RegisterFailed);
            Topics = _topicRepo.GetMyTopicAndProfessorTopics(SessionInfo.UserId);
            RegisterNewTopicCommand.RaiseCanExecuteChanged();

            currentView?.Close();
            var mainWindow = Application.Current.MainWindow;
            mainWindow.Focus();
        }

        private bool CanCreateOrUpdateTopic(object obj)
        {
            return true;
        }

        private bool CanShowTopicView(object obj)
        {
            if (SessionInfo.Role == Role.Student)
            {
                return CanStudentRegisterTopic(obj);
            }
            return true;
        }

        private void ExecuteShowTopicView(object sender)
        {
            ResetTopicProperties();
            Window topicView;
            if (SessionInfo.Role == Role.Student)
            {
                topicView = new StudentTopicView();
                StudentId = SessionInfo.UserId;
                currentUser = Students.FirstOrDefault(s => s.Id == SessionInfo.UserId);
                Students.Remove((Student)currentUser);
                SelectedStudents.Add((Student)currentUser);
                SelectedStudentNames = SessionInfo.Name;
            }
            else
            {
                topicView = new ProfessorTopicView();
                ProfessorId = SessionInfo.UserId;
            }
            topicView.DataContext = this;
            topicView.Owner = Application.Current.MainWindow;
            topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            topicView.Show();
        }

        private void ExecuteCreateOrUpdateCommand(object obj)
        {
            if (IsTopicNotValid())
                return;

            UpdateSelectedTopicProperties();
            Window? topicView = obj as Window;

            if (id <= 0)
            {
                var success = _topicRepo.Add(selectedTopic);
                ShowMessage(success, Message.AddSuccess, Message.AddFailed);
            }
            else
            {
                var success = _topicRepo.Update(selectedTopic);
                ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
            }

            Topics = _topicRepo.GetAll(SessionInfo.UserId);
            topicView?.Close();
            var mainWindow = Application.Current.MainWindow;
            mainWindow.Focus();
        }

        private void ExecuteDeleteCommand(object parameter)
        {
            var confirmed = ConfirmDelete();
            if (confirmed == MessageBoxResult.Yes)
            {
                var success = _topicRepo.Delete(id);
                ShowMessage(success, Message.DeleteSuccess, Message.DeleteFailed);
                Topics = _topicRepo.GetAll(SessionInfo.UserId);
            }
        }

        private bool CanExecuteDelete(object parameter)
        {
            int id = (int)parameter;
            return _topicRepo.CanBeDeleted(id);
        }

        private void FilterData()
        {
            Topics = _topicRepo.GetFilteredTopics(FilterCategory, FilterTechnology, FilterProfessorName);
        }

        private void FilterStudent()
        {
            Students = Students.OrderByDescending(s => s.Name.ToLower().Contains(filterStudent) || s.Id.Contains(filterStudent)).ToList();
        }

        private void ResetTopicProperties()
        {
            SelectedTopic = new Topic();
            Id = 0;
            Name = "";
            Description = "";
            Requirement = "";
            Category = "";
            Technology = "";
            Function = "";
            ProfessorId = "";
            studentQuantity = 1;
        }

        private void UpdateSelectedTopicProperties()
        {
            selectedTopic.Id = id;
            selectedTopic.Name = name;
            selectedTopic.ProfessorId = professorId;
            selectedTopic.StudentId = studentId;
            selectedTopic.Category = category;
            selectedTopic.Technology = technology;
            selectedTopic.Description = description;
            selectedTopic.Requirement = requirement;
            selectedTopic.Function = function;
            selectedTopic.StudentQuantity = studentQuantity;
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