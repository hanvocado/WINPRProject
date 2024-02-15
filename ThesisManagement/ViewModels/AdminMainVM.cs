using System.Windows.Input;

namespace ThesisManagement.ViewModels
{
    public class AdminMainVM : ViewModelBase
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

        public ICommand ShowAdminAccountView { get; set; }
        public ICommand ShowProfessorsView { get; set; }
        public AdminMainVM()
        {
            ShowAdminAccountView = new ViewModelCommand(ExecuteShowAdminAccountView);
            ShowProfessorsView = new ViewModelCommand(ExecuteShowProfessorAccountView);

            ExecuteShowAdminAccountView(null);
        }

        private void ExecuteShowProfessorAccountView(object? obj)
        {
            CurrentChildView = new ProfessorsVM();
        }

        private void ExecuteShowAdminAccountView(object? obj)
        {
            CurrentChildView = new AdminsVM();
        }
    }
}
