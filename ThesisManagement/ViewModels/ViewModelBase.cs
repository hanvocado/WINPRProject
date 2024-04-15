﻿using HandyControl.Data;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using ThesisManagement.CustomControls;
using ThesisManagement.Helpers;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Shared;

namespace ThesisManagement.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public bool IsProfessor
        {
            get { return SessionInfo.Role == Role.Professor; }
        }

        Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();
        public bool HasErrors => Errors.Count > 0;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (String.IsNullOrEmpty(propertyName) || !Errors.ContainsKey(propertyName))
                return Enumerable.Empty<string>();

            return Errors[propertyName];
        }

        public void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        protected bool Validate(string propertyName, object propertyValue, ViewModelCommand? cmd)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);


            if (results.Any())
            {
                Errors[propertyName] = results.Select(r => r.ErrorMessage).ToList();
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                Errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

            if (cmd != null)
                cmd.RaiseCanExecuteChanged();

            return !results.Any();
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

        public MessageBoxResult ConfirmDelete()
        {
            MessageBoxResult result = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButton.YesNo);
            return result;
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
    }
}
