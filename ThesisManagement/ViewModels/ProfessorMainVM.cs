using System.Windows.Input;

namespace ThesisManagement.ViewModels
{
    public class ProfessorMainVM : ViewModelBase
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

        public ICommand ShowTopicView { get; set; }
        public ICommand ShowStudentView { get; set; }
        public ICommand ShowThesisView { get; set; }

        public ProfessorMainVM()
        {
            ShowTopicView = new ViewModelCommand(ExecuteShowTopicView);
            ShowStudentView = new ViewModelCommand(ExecuteShowStudentView);
            ShowThesisView = new ViewModelCommand(ExecuteShowThesisView);

            ExecuteShowTopicView(null);
        }

        private void ExecuteShowThesisView(object? obj)
        {
            CurrentChildView = new ThesesVM();
        }

        private void ExecuteShowStudentView(object? obj)
        {
            CurrentChildView = new StudentsVM();
        }

        private void ExecuteShowTopicView(object? obj)
        {
            CurrentChildView = new TopicsVM();
        }
    }
}
