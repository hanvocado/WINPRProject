using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ThesisManagement.ViewModels
{
    public class StudentMainVM:ViewModelBase
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

        public StudentMainVM()
        {
            ShowTopicsView = new ViewModelCommand(ExecuteShowTopicsView);
            ShowThesisView = new ViewModelCommand(ExecuteShowThesisView);
            ShowStudentProfileView = new ViewModelCommand(ExcututeShowStudentProfileView);

            ExecuteShowTopicsView(null);
        }

        private void ExcututeShowStudentProfileView(object obj)
        {
            CurrentChildView = new ProfessorProfileVM();
        }

        private void ExecuteShowThesisView(object? obj)
        {
            CurrentChildView = new ThesesVM();
        }

        private void ExecuteShowTopicsView(object? obj)
        {
            CurrentChildView = new TopicsVM();
        }
    }
}
