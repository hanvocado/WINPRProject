using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ThesisManagement.Models;

namespace ThesisManagement.ViewModels
{
    internal class ProfessorProfileVM : ViewModelBase
    {

        public ICommand UpdateCommand { get; set; }

        public ProfessorProfileVM()
        {
            UpdateCommand = new ViewModelCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand);
        }

        private bool CanExecuteUpdateCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteUpdateCommand(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
