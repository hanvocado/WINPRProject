using System.Windows.Input;

namespace ThesisManagement.ViewModels
{
    public class AdminVM : ViewModelBase
    {
        private Guid _id;
        private string _email;
        private bool _isViewVisible = true;

        public Guid Id 
        { 
            get => _id; 
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
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
        
        public bool IsViewVisible 
        { 
            get => _isViewVisible; 
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public ICommand CreateCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public AdminVM()
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
