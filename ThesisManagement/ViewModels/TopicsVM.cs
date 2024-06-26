﻿using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using ThesisManagement.CustomControls;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Student;
using ProfessorTopicView = ThesisManagement.Views.Professor.TopicView;
using StudentTopicView = ThesisManagement.Views.Student.TopicView;

namespace ThesisManagement.ViewModels
{
    public class TopicsVM : ViewModelBase
    {
        private readonly ITopicRepository _topicRepo;
        private readonly IProfessorRepository _professorRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IThesisRepository _thesisRepo;
        private readonly DialogService _dialogService;

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
                Topics = _topicRepo.Get(value, SessionInfo.UserId);
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

        public IEnumerable<string> Categories { get; set; } = Models.Category.GetCategories();
        public IEnumerable<Technology> Technologies { get; set; } = Models.Technology.GetTechnologies();

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

        private IEnumerable<Technology> selectedTechnologies;
        public IEnumerable<Technology> SelectedTechnologies
        {
            get { return selectedTechnologies; }
            set
            {
                selectedTechnologies = value;
                OnPropertyChanged(nameof(SelectedTechnologies));
            }
        }

        private bool isTechnologyFocused;

        public bool IsTechnologyFocused
        {
            get { return isTechnologyFocused; }
            set { isTechnologyFocused = value; OnPropertyChanged(nameof(IsTechnologyFocused)); }
        }


        public ViewModelCommand ShowTopicViewCommand { get; set; }
        public ViewModelCommand CreateOrUpdateCommand { get; set; }
        public ViewModelCommand DeleteCommand { get; set; }
        public ViewModelCommand SaveCommand { get; set; }
        public ViewModelCommand RegisterTopicCommand { get; set; }
        public ViewModelCommand RegisterNewTopicCommand { get; set; }
        public ViewModelCommand ChooseMembersCommand { get; set; }
        public ViewModelCommand AddMembersCommand { get; set; }
        public ViewModelCommand SelectTechnologiesCommand { get; set; }
        public ViewModelCommand CancelPopupCommand { get; set; }

        public TopicsVM()
        {
            _topicRepo = new TopicRepository();
            _professorRepo = new ProfessorRepository();
            _studentRepo = new StudentRepository();
            _thesisRepo = new ThesisRepository();
            _dialogService = new DialogService();
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
            SelectTechnologiesCommand = new ViewModelCommand(ExecuteSelectTechnologies);
            CancelPopupCommand = new ViewModelCommand(ExecuteCancelPopupCommand);
        }

        private void ExecuteCancelPopupCommand(object obj)
        {
            ((Popup)obj).IsOpen = false;
        }

        private void ExecuteSelectTechnologies(object obj)
        {
            SelectedTechnologies = Technologies.Where(s => s.IsSelected);
            Technology = String.Join(" - ", selectedTechnologies.Select(t => t.Name));
            Popup? selectTechPopup = obj as Popup;
            if (selectTechPopup != null)
            {
                selectTechPopup.IsOpen = false;
            }
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
                ShowMessage(false, null, Message.ExceedStudentQuantity);
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
            ValidateInput();
            if (existError)
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
                ShowMessage(false, null, Message.ExceedStudentQuantity);
                return;
            }
            else
            {
                bool? confirmRegister = _dialogService.ShowDialog(Message.Notification, Message.RegisterTopicNotification);
                if (confirmRegister == true)
                {
                    RegisterTopic(obj);
                }
            }
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
            if (SessionInfo.Role == Role.Professor)
                return SessionInfo.UserId == professorId;
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
                var currentStudent = Students.FirstOrDefault(s => s.Id == SessionInfo.UserId);
                Students.Remove(currentStudent);
                SelectedStudents.Add(currentStudent);
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
            ValidateInput();
            if (existError)
                return;

            UpdateSelectedTopicProperties();
            Window? topicView = obj as Window;

            if (id <= 0)
            {
                bool? confirmAdd = _dialogService.ShowDialog(Message.Notification, Message.AddTopicNotification);
                if (confirmAdd == true)
                {
                    var success = _topicRepo.Add(selectedTopic);
                    ShowMessage(success, Message.AddSuccess, Message.AddFailed);
                }
            }
            else
            {
                bool? confirmUpdate = _dialogService.ShowDialog(Message.Notification, Message.UpdateTopicNotification);
                if (confirmUpdate == true)
                {
                    var success = _topicRepo.Update(selectedTopic);
                    ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                }
            }

            Topics = _topicRepo.GetAll(SessionInfo.UserId);
            topicView?.Close();
            var mainWindow = Application.Current.MainWindow;
            mainWindow.Focus();
        }

        private void ExecuteDeleteCommand(object parameter)
        {
            bool? confirmDelete = _dialogService.ShowDialog(Message.Notification, Message.DeleteTopicNotification);
            if (confirmDelete == true)
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
            Topics = _topicRepo.Get(FilterCategory, FilterTechnology, FilterProfessorName);
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
            SelectedTechnologies = new List<Technology>();
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

        private void ValidateInput()
        {
            this.ExistError = String.IsNullOrEmpty(professorId) || String.IsNullOrEmpty(name)
                            || String.IsNullOrEmpty(technology) || String.IsNullOrEmpty(category)
                            || String.IsNullOrEmpty(description) || String.IsNullOrEmpty(function)
                            || studentQuantity < 1;
            if (existError)
                ShowMessage(false, null, Message.RequiredError);
        }
    }
}