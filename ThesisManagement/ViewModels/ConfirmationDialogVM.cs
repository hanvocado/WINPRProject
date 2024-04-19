using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ThesisManagement.ViewModels
{
    public class ConfirmationDialogVM : ViewModelBase
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(nameof(Title)); }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged(nameof(Message)); }
        }

        private bool? dialogResult;
        public bool? DialogResult
        {
            get { return dialogResult; }
            set
            {
                dialogResult = value;
                OnPropertyChanged(nameof(DialogResult));
            }
        }

        public ICommand YesCommand { get; set; }
        public ICommand NoCommand { get; set; }

    }
}
