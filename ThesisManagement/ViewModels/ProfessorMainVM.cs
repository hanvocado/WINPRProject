using System.Windows;
using System.Windows.Input;
using ThesisManagement.Helpers;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Shared;

namespace ThesisManagement.ViewModels
{
    public class ProfessorMainVM : ViewModelBase
    {
        private readonly IProfessorRepository _profRepo;
        private readonly IThesisRepository _thesisRepo;
        private ViewModelBase _currentChildView;

        public ViewModelBase CurrentChildView
        {
            get => _currentChildView;
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        private bool hasNewUpdate;

        public bool HasNewUpdate
        {
            get { return hasNewUpdate; }
            set { hasNewUpdate = value; OnPropertyChanged(nameof(HasNewUpdate)); }
        }

        private int? registerCount;

        public int? RegisterCount
        {
            get { return registerCount; }
            set { registerCount = value; OnPropertyChanged(nameof(RegisterCount)); }
        }


        public ICommand ShowTopicsView { get; set; }
        public ICommand ShowWaitingStudentsView { get; set; }
        public ICommand ShowThesesView { get; set; }
        public ICommand ShowProfessorProfileView { get; set; }
        public ICommand LogoutCommand { get; set; }

        public ProfessorMainVM()
        {
            _profRepo = new ProfessorRepository();
            _thesisRepo = new ThesisRepository();

            RegisterCount = _thesisRepo.Get(SessionInfo.UserId, Variable.StatusTopic.Waiting).Count();
            if (RegisterCount == 0)
            {
                RegisterCount = null;
            }
            hasNewUpdate = _profRepo.HasNewUpdate(SessionInfo.UserId);

            ShowTopicsView = new ViewModelCommand(ExecuteShowTopicsView);
            ShowWaitingStudentsView = new ViewModelCommand(ExecuteShowWaitingStudentsView);
            ShowThesesView = new ViewModelCommand(ExecuteShowThesesView);
            ShowProfessorProfileView = new ViewModelCommand(ExecuteShowProfileView);
            LogoutCommand = new ViewModelCommand(ExcuteLogout);
            ExecuteShowTopicsView(null);
        }

        private void ExcuteLogout(object obj)
        {
            var currentWindow = Application.Current.MainWindow;
            var loginWindow = new LoginView();
            Application.Current.MainWindow = loginWindow;
            loginWindow.Show();
            currentWindow.Close();
            SessionInfo.Clear();
        }

        private void ExecuteShowProfileView(object obj)
        {
            CurrentChildView = new ProfileVM();
        }

        private void ExecuteShowWaitingStudentsView(object? obj)
        {
            CurrentChildView = new ReviewThesesVM();
        }

        private void ExecuteShowThesesView(object? obj)
        {
            CurrentChildView = new ThesesVM();
        }

        private void ExecuteShowTopicsView(object? obj)
        {
            CurrentChildView = new TopicsVM();
        }
    }
}
