using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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

        private readonly Role currentUserRole;
        private User currentUser;

        private Topic? selectedTopic;
        private List<Student> selectedStudents;

        public Topic? SelectedTopic
        {
            get { return selectedTopic; }
            set { selectedTopic = value; OnPropertyChanged(nameof(SelectedTopic)); }
        }


        private ObservableCollection<Topic> topics;

        private ObservableCollection<Professor> professors;
        private List<Student> students;

        private string? filterTopicName;
        private string? filterProfessorName;
        private string? filterCategory;
        private string? filterTechnology;
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

        private string? function;
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

        public string FilterTopicName
        {
            get { return filterTopicName; }
            set
            {
                filterTopicName = value;
                OnPropertyChanged(nameof(FilterTopicName));
                Topics = _topicRepo.GetByTopicName(value);
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

        private string selectedStudentNames;

        public string SelectedStudentNames
        {
            get { return selectedStudentNames; }
            set { selectedStudentNames = value; OnPropertyChanged(nameof(SelectedStudentNames)); }
        }


        public IEnumerable<string> Categories
        { get; set; } = new List<string>() { "Computer Science", "Web Development", "Data Science", "Other" };
        public IEnumerable<string> Technologies { get; set; } = new List<string>() { "JavaScript", "Wpf", ".NET", "Java", "Python", "SQL", "ASP.NET Core", "Other" };
        public IEnumerable<string> ProfessorNames { get; set; }
        public ViewModelCommand ProfessorCreateTopic { get; set; }
        public ViewModelCommand StudentCreateTopic { get; set; }
        public ViewModelCommand CreateOrUpdateCommand { get; set; }
        public ViewModelCommand DeleteCommand { get; set; }
        public ViewModelCommand SaveCommand { get; set; }
        public ViewModelCommand RegisterThesisCommand { get; set; }
        public ViewModelCommand RegisterNewTopicCommand { get; set; }
        public ViewModelCommand ChooseMembersCommand { get; set; }
        public ViewModelCommand AddMembersCommand { get; set; }

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

        public List<Student> SelectedStudents
        {
            get { return selectedStudents; }
            set
            {
                selectedStudents = value;
                OnPropertyChanged(nameof(SelectedStudents));
            }
        }

        public List<Student> Students
        {
            get { return students; }
            set
            {
                students = value;
                OnPropertyChanged(nameof(Students));
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
            currentUserRole = SessionInfo.Role;
            studentFilter = "";
            _topicRepo = new TopicRepository();
            _professorRepo = new ProfessorRepository();
            _studentRepo = new StudentRepository();
            _thesisRepo = new ThesisRepository();
            SelectedStudents = new List<Student>();

            if (currentUserRole == Role.Professor)
            {
                Topics = _topicRepo.GetAll(SessionInfo.UserId);
            }
            else
                Topics = _topicRepo.GetMyTopicsAndProfessorTopics(SessionInfo.UserId);
            Students = _studentRepo.GetUnRegisteredStudents();
            Professors = _professorRepo.GetAll();
            ProfessorCreateTopic = new ViewModelCommand(ExecuteProfessorCreateCommand);
            StudentCreateTopic = new ViewModelCommand(ExecuteStudentCreateCommand, CanStudentRegisterTopic);
            CreateOrUpdateCommand = new ViewModelCommand(ExecuteCreateOrUpdateCommand, CanCreateOrUpdateTopic);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDelete);
            RegisterThesisCommand = new ViewModelCommand(ExecuteRegisterThesisCommand, CanStudentRegisterTopic);
            RegisterNewTopicCommand = new ViewModelCommand(ExecuteRegisterNewTopicCommand, CanStudentRegisterTopic);
            ChooseMembersCommand = new ViewModelCommand(ExecuteChooseMembers, CanStudentRegisterTopic);
            AddMembersCommand = new ViewModelCommand(ExecuteAddMembers);
        }

        private bool CanStudentRegisterTopic(object obj)
        {
            return true;
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
            Trace.WriteLine("Danh sách sv đã chọn");
            foreach (var mem in SelectedStudents)
            {
                Trace.WriteLine($"{mem.Name}");
            }
            if (selectedStudents.Count > selectedTopic?.StudentQuantity)
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
            StudentTopicView topicView = obj as StudentTopicView;
            Topic topic = new Topic
            {
                Id = id,
                Name = name,
                ProfessorId = professorId,
                StudentId = SessionInfo.UserId,
                Category = category,
                Technology = technology,
                Description = description
            };

            var isSuccess = _topicRepo.Add(topic);
            if (isSuccess)
            {
                Thesis thesis = new Thesis
                {
                    TopicId = topic.Id,
                    TopicStatus = Variable.StatusTopic.Waiting,
                    File = null,
                    Score = 0
                };
                isSuccess = _thesisRepo.Add(thesis);
                foreach (var student in SelectedStudents)
                {
                    student.ThesisId = thesis.Id;
                    _studentRepo.Update(student);
                }
            }
            ShowMessage(isSuccess, Message.RegisterSuccess, Message.RegisterFailed);

            topicView?.Close();

            Topics = _topicRepo.GetMyTopicsAndProfessorTopics(SessionInfo.UserId);
            var mainWindow = Application.Current.MainWindow;
            mainWindow.Focus();
        }

        private void ExecuteRegisterThesisCommand(object obj)
        {
            if (selectedStudents.Count > selectedTopic?.StudentQuantity)
            {
                ShowMessage(false, "", Message.ExceedStudentQuantity);
                return;
            }
            RegisterTopicView registerView = obj as RegisterTopicView;
            Thesis thesis = new Thesis
            {
                TopicId = SelectedTopic.Id,
                TopicStatus = Variable.StatusTopic.Waiting,
                File = null,
            };
            var success = _thesisRepo.Add(thesis);
            Trace.WriteLine("Thay đối data danh sách sv đã chọn");
            foreach (var mem in SelectedStudents)
            {
                Trace.WriteLine($"{mem.Name}");
            }
            if (success)
                foreach (var student in SelectedStudents)
                {
                    student.ThesisId = thesis.Id;
                    Trace.WriteLine($"{student.Name} - {student.ThesisId}");
                    success = _studentRepo.Update(student);
                }

            ShowMessage(success, Message.RegisterSuccess, Message.RegisterFailed);

            registerView?.Close();

            var mainWindow = Application.Current.MainWindow;
            mainWindow.Focus();
        }

        private bool CanCreateOrUpdateTopic(object obj)
        {
            return true;
        }

        private void ExecuteProfessorCreateCommand(object sender)
        {
            ProfessorTopicView topicView = new();
            ResetTopicProperties();
            this.ProfessorId = SessionInfo.UserId;
            topicView.DataContext = this;
            topicView.Owner = Application.Current.MainWindow;
            topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            topicView.Show();
        }

        private void ExecuteStudentCreateCommand(object sender)
        {
            StudentTopicView topicView = new();
            ResetTopicProperties();
            this.StudentId = SessionInfo.UserId;
            topicView.DataContext = this;

            currentUser = Students.FirstOrDefault(s => s.Id == SessionInfo.UserId);
            Students.Remove((Student)currentUser);
            SelectedStudents.Add((Student)currentUser);
            SelectedStudentNames = currentUser.Name;

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
                Function = function,
                StudentQuantity = studentQuantity
            };

            if (id <= 0)
            {
                var success = _topicRepo.Add(topic);
                ShowMessage(success, Message.AddSuccess, Message.AddFailed);
            }
            else
            {
                var success = _topicRepo.Update(topic);
                ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
            }

            Topics = _topicRepo.GetAll(SessionInfo.UserId);

            topicView?.Close();

            var mainWindow = Application.Current.MainWindow;
            mainWindow.Focus();
        }

        private void ExecuteDeleteCommand(object parameter)
        {
            var success = _topicRepo.Delete(id);
            ShowMessage(success, Message.DeleteSuccess, Message.DeleteFailed);
            Topics = _topicRepo.GetAll(SessionInfo.UserId);
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
            Students = Students.OrderByDescending(s => s.Name.ToLower().Contains(studentFilter) || s.Id.Contains(studentFilter)).ToList();
        }

        private void ResetTopicProperties()
        {
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