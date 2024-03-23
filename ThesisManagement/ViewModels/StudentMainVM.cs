using System.Windows;
using System.Windows.Input;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Shared;

namespace ThesisManagement.ViewModels
{
    public class StudentMainVM : ViewModelBase
    {
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
        public ICommand ShowTopicsView { get; set; }
        public ICommand ShowThesisView { get; set; }
        public ICommand ShowStudentProfileView { get; set; }

        public ICommand LogoutCommand { get; set; }

        public StudentMainVM()
        {
            ShowTopicsView = new ViewModelCommand(ExecuteShowTopicsView);
            ShowThesisView = new ViewModelCommand(ExecuteShowThesisView);
            ShowStudentProfileView = new ViewModelCommand(ExcututeShowStudentProfileView);
            LogoutCommand = new ViewModelCommand(ExcuteLogout);

            ExecuteShowTopicsView(null);
        }

        private void ExcututeShowStudentProfileView(object obj)
        {
            CurrentChildView = new ProfessorProfileVM();
        }

        private void ExecuteShowThesisView(object? obj)
        {
            CurrentChildView = new MyThesisVM();
        }

        private void ExecuteShowTopicsView(object? obj)
        {
            CurrentChildView = new TopicsViewModel();
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
    }
}
