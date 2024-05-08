using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ThesisManagement.CustomControls;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Shared;

namespace ThesisManagement.ViewModels
{
    internal class ProfileVM : ViewModelBase
    {
        private readonly IProfessorRepository _professorRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly DialogService _dialogService;
        private string appDirectory;
        private string defaultAvatar = "\\Resources\\Images\\default_avatar.jpg";

        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public ICommand ShowUpdateProfileCommand { get; set; }
        public ICommand UpdateProfileCommand { get; set; }
        public ICommand UpdateAvatarCommand { get; set; }

        public ProfileVM()
        {
            _professorRepo = new ProfessorRepository();
            _studentRepo = new StudentRepository();
            _dialogService = new DialogService();
            appDirectory = SessionInfo.BinDirectory;
            Load();

            ShowUpdateProfileCommand = new ViewModelCommand(ExecuteShowUpdateProfileCommand);
            UpdateProfileCommand = new ViewModelCommand(ExecuteUpdateProfileCommand);
            UpdateAvatarCommand = new ViewModelCommand(ExecuteUpdateAvatarCommand);
        }

        private void ExecuteUpdateAvatarCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                string extension = Path.GetExtension(fileName);
                if (imageExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
                {
                    string userFileName = SessionInfo.UserId + Path.GetFileName(fileName);
                    var destinationPath = Path.Combine(appDirectory, userFileName);
                    File.Copy(fileName, destinationPath, true);

                    User.Avatar = destinationPath;
                    if (SessionInfo.Role == Role.Professor)
                    {
                        var success = _professorRepo.Update((Professor)user);
                        ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                    }
                    else
                    {
                        var success = _studentRepo.Update((Student)user);
                        ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                    }
                }
                else
                {
                    _dialogService.ShowDialog(Message.ErrorNotification, Message.ImageOnly);
                }
            }
            Load();
        }

        private void ExecuteShowUpdateProfileCommand(object obj)
        {
            Window updateProfileView = new UpdateProfileView { DataContext = this};
            updateProfileView.Owner = Application.Current.MainWindow;
            updateProfileView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            updateProfileView.Show();
        }

        private void ExecuteUpdateProfileCommand(object obj)
        {
            bool? confirmUpdateProfile = _dialogService.ShowDialog(Message.Notification, Message.ContinueNotification);
            if (confirmUpdateProfile == true)
            {
                UpdateProfileView? updateProfileView = obj as UpdateProfileView;
                if (SessionInfo.Role == Role.Professor)
                {
                    var success = _professorRepo.Update((Professor)user);
                    ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                }    
                else
                {
                    var success = _studentRepo.Update((Student)user);
                    ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                }    
                updateProfileView?.Close();
                Load();
            }
        }

        private void Load()
        {
            if (SessionInfo.Role == Role.Professor)
                User = _professorRepo.Get(SessionInfo.UserId);
            else
                User = _studentRepo.GetStudent(SessionInfo.UserId);
            if (string.IsNullOrEmpty(user.Avatar))
                User.Avatar = defaultAvatar;
        }
    }
}
