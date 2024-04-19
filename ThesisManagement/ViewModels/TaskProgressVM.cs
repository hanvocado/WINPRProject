using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Services;
using ThesisManagement.Views.Shared;

namespace ThesisManagement.ViewModels
{
    public class TaskProgressVM : ViewModelBase
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IAttachmentRepository _attachmentRepo;
        private readonly ITaskRepository _taskRepo;
        private readonly IThesisRepository _thesisRepo;
        private readonly ITaskProgressRepository _taskProgressRepo;
        private readonly IDialogService _dialogService;
        private string appDirectory;
        public string userAttachmentName;

        private int taskId;
        public int TaskId
        {
            get { return taskId; }
            set
            {
                taskId = value;
                OnPropertyChanged(nameof(TaskId));
            }
        }

        private string studentId;

        public string StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }


        private string? studentName;
        public string? StudentName
        {
            get { return studentName; }
            set
            {
                studentName = value;
                OnPropertyChanged(nameof(StudentName));
            }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int progress;
        public int Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string response;
        public string Response
        {
            get { return response; }
            set
            {
                response = value;
                OnPropertyChanged(nameof(Response));
            }
        }

        private DateTime? updateAt;

        public DateTime? UpdateAt
        {
            get { return updateAt; }
            set { updateAt = value; OnPropertyChanged(nameof(UpdateAt)); }
        }

        public bool IsStudent
        {
            get
            {
                if (SessionInfo.Role == Role.Student)
                    return true;
                else
                    return false;
            }
        }

        private Attachment selectedFile;

        public Attachment SelectedFile
        {
            get { return selectedFile; }
            set
            {
                if (value != null)
                {
                    selectedFile = value;
                    OnPropertyChanged(nameof(SelectedFile));
                    StartProcess(selectedFile.FileName);
                }
            }
        }

        private TaskProgressHistoryVM parentVM;

        public TaskProgressHistoryVM ParentVM
        {
            get { return parentVM; }
            set { parentVM = value; }
        }

        private List<Attachment> attachments;
        public List<Attachment> Attachments
        {
            get { return attachments; }
            set
            {
                attachments = value;
                OnPropertyChanged(nameof(Attachments));
            }
        }

        public ICommand UpdateTaskProgressCommand { get; set; }
        public ICommand UploadAttachmentCommand { get; set; }

        public TaskProgressVM()
        {
            _taskRepo = new TaskRepository();
            _thesisRepo = new ThesisRepository();
            _studentRepo = new StudentRepository();
            _attachmentRepo = new AttachmentRepository();
            _taskProgressRepo = new TaskProgressRepository();
            _dialogService = new DialogService();
            appDirectory = SessionInfo.BinDirectory;
            Attachments = new List<Attachment>();

            UpdateTaskProgressCommand = new ViewModelCommand(ExecuteUpdateTaskProgressCommand);
            UploadAttachmentCommand = new ViewModelCommand(ExecuteUploadAttachmentCommand);
        }

        private void ExecuteUploadAttachmentCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                string[] attachmentNames = openFileDialog.FileNames;

                foreach (string attachmentName in attachmentNames)
                {
                    userAttachmentName = DateTime.Now.ToString("ddMMyyyyhhmmss") + Path.GetFileName(attachmentName);

                    Attachment attachment = new Attachment();
                    attachment.FileName = userAttachmentName;
                    Attachments.Add(attachment);

                    string destinationPath = Path.Combine(appDirectory, userAttachmentName);
                    File.Copy(attachmentName, destinationPath, true);
                }
            }
        }

        private void ExecuteUpdateTaskProgressCommand(object obj)
        {
            bool? confirmUpdateProgress = _dialogService.ShowDialog(Message.Notification, Message.UpdateTaskProgressNotification);
            if (confirmUpdateProgress == true)
            {
                UpdateTaskProgressView? taskProgressView = obj as UpdateTaskProgressView;
            if (SessionInfo.Role == Role.Student)
            {
                CreateNewProgress();
            }
            else if (SessionInfo.Role == Role.Professor)
            {
                UpdateProgress();
            }
            parentVM?.Reload();
            parentVM?.ParentTasksVM?.Reload();
            taskProgressView?.Close();
            }

        }

        private void CreateNewProgress()
        {
            var newProgress = new TaskProgress
            {
                Id = id,
                TaskId = taskId,
                Description = description,
                StudentId = studentId,
                Progress = progress,
                Response = null,
                UpdateAt = DateTime.Now,
            };
            var addSuccess = _taskProgressRepo.Add(newProgress);
            if (addSuccess)
            {
                addSuccess = UpdateAttachments(newProgress.Id);
                var task = _taskRepo.GetTask(taskId);
                task.WaitingForResponse += 1;
                _taskRepo.Update(task);
            }
            ShowMessage(addSuccess, Message.AddSuccess, Message.AddFailed);
        }

        private void UpdateProgress()
        {
            var success = _taskProgressRepo.Update(id, response);
            if (success)
            {
                UpdateAttachments(id);
                var acceptedTask = _taskRepo.GetTask(taskId);
                acceptedTask.Progress = progress;
                acceptedTask.WaitingForResponse -= 1;
                _taskRepo.Update(acceptedTask);
                Window profWindow = Application.Current.MainWindow;
                profWindow.DataContext = new ProfessorMainVM { CurrentChildView = new ThesesVM() };
            }
            ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
        }

        public bool UpdateAttachments(int progressId)
        {
            if (Attachments.Count() > 0)
            {
                foreach (var attachment in Attachments)
                {
                    attachment.TaskProgressId = progressId;
                }
                return _attachmentRepo.AddRange(Attachments);
            }
            return true;
        }
    }
}
