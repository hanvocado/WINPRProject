using System.Windows;
using System.Windows.Input;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Shared;

namespace ThesisManagement.ViewModels
{
    public class ProfessorMainVM : ViewModelBase
    {
        private readonly IProfessorRepository _profRepo;
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

        private string updateCount;

        public string? UpdateCount
        {
            get { return updateCount; }
            set { updateCount = value; OnPropertyChanged(nameof(UpdateCount)); }
        }


        public ICommand ShowTopicsView { get; set; }
        public ICommand ShowStudentView { get; set; }
        public ICommand ShowThesisView { get; set; }
        public ICommand ShowProfessorProfileView { get; set; }
        public ICommand LogoutCommand { get; set; }

        public ProfessorMainVM()
        {
            _profRepo = new ProfessorRepository();
            UpdateCount = _profRepo.NoStudentUpdates(SessionInfo.UserId);
            ShowTopicsView = new ViewModelCommand(ExecuteShowTopicsView);
            ShowStudentView = new ViewModelCommand(ExecuteShowStudentView);
            ShowThesisView = new ViewModelCommand(ExecuteShowThesisView);
            ShowProfessorProfileView = new ViewModelCommand(ExcututeShowProfessorProfileView);
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

        private void ExcututeShowProfessorProfileView(object obj)
        {
            CurrentChildView = new ProfessorProfileVM();
        }

        private void ExecuteShowThesisView(object? obj)
        {
            CurrentChildView = new ThesesVM();
        }

        private void ExecuteShowStudentView(object? obj)
        {
            CurrentChildView = new StudentsVM();
        }

        private void ExecuteShowTopicsView(object? obj)
        {
            CurrentChildView = new TopicsVM();
        }
    }
}
