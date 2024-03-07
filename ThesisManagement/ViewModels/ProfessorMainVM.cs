using System.Windows.Input;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.ViewModels
{
    public class ProfessorMainVM : ViewModelBase
    {
        private ViewModelBase _currentChildView;
        private AppDbContext _context;

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
        public ICommand ShowStudentView { get; set; }
        public ICommand ShowThesisView { get; set; }
        public ICommand ShowProfessorProfileView { get; set; }

        public ProfessorMainVM(AppDbContext context)
        {
            _context = context;
            ShowTopicsView = new ViewModelCommand(ExecuteShowTopicsView);
            ShowStudentView = new ViewModelCommand(ExecuteShowStudentView);
            ShowThesisView = new ViewModelCommand(ExecuteShowThesisView);
            ShowProfessorProfileView = new ViewModelCommand(ExcututeShowProfessorProfileView);

            ExecuteShowTopicsView(null);
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
            CurrentChildView = new TopicsVM(_context);
        }
    }
}
