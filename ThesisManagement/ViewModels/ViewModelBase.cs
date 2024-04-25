using HandyControl.Data;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using ThesisManagement.CustomControls;
using ThesisManagement.Helpers;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Shared;

namespace ThesisManagement.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public bool IsProfessor
        {
            get { return SessionInfo.Role == Role.Professor; }
        }

        public bool IsStudent
        {
            get { return SessionInfo.Role == Role.Student; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void ShowMessage(bool success, string successMessage, string failedMessage)
        {
            if (success)
            {
                var notif = new SuccessNotify(successMessage);
                Notification.Show(notif, ShowAnimation.HorizontalMove, false);
            }
            else
            {
                var notif = new FailedNotify(failedMessage);
                Notification.Show(notif, ShowAnimation.HorizontalMove, false);
            }
        }

        public void StartProcess(string fileName)
        {
            string filePath = Path.Combine(SessionInfo.BinDirectory, fileName);
            if (File.Exists(filePath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo { FileName = filePath, UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    ShowMessage(false, null, ex.Message);
                }
            }
            else
            {
                ShowMessage(false, null, Message.FileNotFound);
            }
        }

        protected bool existError;
        public bool ExistError
        {
            get { return existError; }
            set
            {
                existError = value;
                OnPropertyChanged(nameof(ExistError));
            }
        }
    }
}
