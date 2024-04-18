using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using System.Xml.Linq;
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
        public TaskProgress lastestTaskProgress;
        public TaskProgress studentTaskProgress;
        public string appDirectory;
        public string destinationPath;
        public string userAttachmentName;

        private int taskId;
        public int TaskId
        {
            get { return taskId; }
            set
            {
                taskId = value;
                OnPropertyChanged(nameof(TaskId));
                if (taskId > 0)
                {
                    lastestTaskProgress = _taskProgressRepo.GetLastestTaskProgress(taskId);
                }
            }
        }

        private Student? student;
        public Student? Student
        {
            get { return student; }
            set
            {
                student = value;
                OnPropertyChanged(nameof(Student));
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

        private TaskProgress selectedTaskProgress;
        public TaskProgress SelectedTaskProgress
        {
            get { return selectedTaskProgress; }
            set
            {
                selectedTaskProgress = value;
                OnPropertyChanged(nameof(SelectedTaskProgress));
            }
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

        private ObservableCollection<Attachment> attachments;
        public ObservableCollection<Attachment> Attachments
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
            SelectedTaskProgress = new TaskProgress();
            attachments = new ObservableCollection<Attachment>();

            UpdateTaskProgressCommand = new ViewModelCommand(ExecuteUpdateTaskProgressCommand);
            UploadAttachmentCommand = new ViewModelCommand(ExecuteUploadAttachmentCommand);
        }

        private void ExecuteUploadAttachmentCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                // Uploaded attachment names (multiple files)
                string[] attachmentNames = openFileDialog.FileNames;

                foreach (string attachmentName in attachmentNames)
                {
                    // Storage attachment name for each file
                    userAttachmentName = DateTime.Now.ToString("ddMMyyyyhhmmss") + Path.GetFileName(attachmentName);

                    Attachment attachment = new Attachment();
                    attachment.FileName = userAttachmentName;
                    attachments.Add(attachment);

                    // Storage path for each attachment
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
                UpdateSelectedTaskProgressProperties();
                if (SessionInfo.Role == Role.Student)
                {
                    //Add TaskProgress and Attachments to db
                    var addSuccess = _taskProgressRepo.Add(studentTaskProgress);
                    lastestTaskProgress = _taskProgressRepo.GetLastestTaskProgress(taskId);
                    var successAttach = UpdateAttachments();
                    ShowMessage(addSuccess && successAttach, Message.AddSuccess, Message.AddFailed);
                }
                else if (SessionInfo.Role == Role.Professor)
                {
                    //Add TaskProgress to db
                    var updateResponse = _taskProgressRepo.Update(selectedTaskProgress);

                    //Update progress in Task to db
                    var acceptedTask = _taskRepo.GetTask(taskId);
                    acceptedTask.Progress = progress;
                    var updateTask = _taskRepo.Update(acceptedTask);

                    //Update attachment to db
                    var successAttach = UpdateAttachments();
                    ShowMessage(updateResponse && updateTask && successAttach, Message.UpdateSuccess, Message.UpdateFailed);
                }
                parentVM?.Reload();
                parentVM?.ParentTasksVM?.Reload();
                taskProgressView?.Close();
            }
            
        }

        public bool UpdateAttachments()
        {
            if (Attachments.Count() > 0)
            {
                foreach (var attachment in Attachments)
                {
                    attachment.TaskProgressId = lastestTaskProgress.Id;
                }
                return _attachmentRepo.AddRange(Attachments);
            }
            return true;
        }

        private void UpdateSelectedTaskProgressProperties()
        {
            if (SessionInfo.Role == Role.Professor)
            {
                selectedTaskProgress.Id = lastestTaskProgress.Id;
                selectedTaskProgress.TaskId = taskId;
                selectedTaskProgress.Response = response;
                selectedTaskProgress.Progress = progress;
            }
            else if (SessionInfo.Role == Role.Student)
            {
                studentTaskProgress = new TaskProgress
                {
                    Id = id,
                    TaskId = taskId,
                    Description = description,
                    StudentId = student.Id,
                    Progress = progress,
                    Response = string.Empty,
                    UpdateAt = DateTime.Now,
                };
            }
        }
    }
}
