using System.Collections.ObjectModel;
using System.Windows.Input;
using ThesisManagement.Models;

namespace ThesisManagement.ViewModels
{
    public class ProfessorsVM : ViewModelBase
    {
        public ObservableCollection<Professor> Professors { get; set; }

        public ICommand CreateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ProfessorsVM()
        {
            CreateCommand = new ViewModelCommand(ExecuteCreateCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
        }

        private void ExecuteDeleteCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteDeleteCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteEditCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteCreateCommand(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
