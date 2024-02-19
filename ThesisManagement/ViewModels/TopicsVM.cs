using System.Windows.Input;

namespace ThesisManagement.ViewModels
{
    public class TopicsVM : ViewModelBase
    {
        private string _name;
        private string _description;
        private DateTime _createAt;
        private DateTime _expiredAt;
        private bool _status;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public ICommand CreateCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public TopicsVM()
        {
            CreateCommand = new ViewModelCommand(ExecuteCreateCommand);
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
        }

        private bool CanExecuteEditCommand(object obj)
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
