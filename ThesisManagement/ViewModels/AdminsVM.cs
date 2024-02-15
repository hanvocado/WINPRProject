using System.Windows.Input;

namespace ThesisManagement.ViewModels
{
    public class AdminsVM : ViewModelBase
    {
        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public ICommand CreateCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public AdminsVM()
        {
            CreateCommand = new ViewModelCommand(ExecuteCreateCommand);
            EditCommand = new ViewModelCommand(ExecuteEditCommand);
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
