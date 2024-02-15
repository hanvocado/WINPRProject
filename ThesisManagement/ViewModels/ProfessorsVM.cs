using System.Windows.Input;

namespace ThesisManagement.ViewModels
{
    public class ProfessorsVM : ViewModelBase
    {
        private string _name;
        private string _email;
        private string _faculty;
        private string _phone;
        private string _birthday;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

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
