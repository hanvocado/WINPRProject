using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
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
        private TasksVM taskVM;
        private Attachment attachment;
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
                Attachments = _attachmentRepo.GetAttachments(selectedTaskProgress.Id);
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


        private List<Attachment>? attachments;
        public List<Attachment>? Attachments
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
            appDirectory = SessionInfo.BinDirectory;
            selectedTaskProgress = new TaskProgress();
            attachment = new Attachment();

            UpdateTaskProgressCommand = new ViewModelCommand(ExecuteUpdateTaskProgressCommand);
            UploadAttachmentCommand = new ViewModelCommand(ExecuteUploadAttachmentCommand);
        }

        private void ExecuteUploadAttachmentCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                //Uploaded attachment name
                string attachmentName = openFileDialog.FileName;
                userAttachmentName = DateTime.Now.ToString("ddMMyyyyhhmmss") + Path.GetFileName(attachmentName);

                //Storage attachment name
                destinationPath = Path.Combine(appDirectory, userAttachmentName);
                File.Copy(attachmentName, destinationPath, true);
            }
        }

        private void ExecuteUpdateTaskProgressCommand(object obj)
        {
            UpdateTaskProgressView? taskProgressView = obj as UpdateTaskProgressView;
            UpdateSelectedTaskProgressProperties();
            if (SessionInfo.Role == Role.Student)
            {
                if (id <= 0)
                {
                    var success = _taskProgressRepo.Add(selectedTaskProgress);
                    ShowMessage(success, Message.AddSuccess, Message.AddFailed);
                }
                else
                {
                    var success = _taskProgressRepo.Update(selectedTaskProgress);
                    ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                }
                UpdateAttachment();
            }
            else if (SessionInfo.Role == Role.Professor)
            {
                var updateTaskProgress = _taskProgressRepo.Update(selectedTaskProgress);
                var acceptedTask = _taskRepo.GetTask(taskId);
                acceptedTask.Progress = progress;
                var updateTask = _taskRepo.Update(acceptedTask);
                ShowMessage(updateTaskProgress && updateTask, Message.UpdateSuccess, Message.UpdateFailed);
                taskVM = new TasksVM();
                taskVM.Thesis = _thesisRepo.GetThesis(selectedTaskProgress.TaskId);
                taskVM.Reload();
            }
            parentVM?.Reload();
            parentVM?.ParentTasksVM?.Reload();
            taskProgressView?.Close();
        }

        public void UpdateAttachment()
        {
            //Update database
            attachment.FileName = userAttachmentName;
            attachment.TaskProgressId = selectedTaskProgress.Id;
            var success = _attachmentRepo.Add(attachment);
        }

        private void UpdateSelectedTaskProgressProperties()
        {
            selectedTaskProgress.Id = id;
            selectedTaskProgress.TaskId = taskId;
            selectedTaskProgress.StudentId = student.Id;
            selectedTaskProgress.Progress = progress;
            selectedTaskProgress.Description = description;
            selectedTaskProgress.Response = response;
            selectedTaskProgress.UpdateAt = DateTime.Now;
        }

        public void UpdateLastestTaskProgress()
        {
            id = selectedTaskProgress.Id;
            selectedTaskProgress.TaskId = taskId;
            Student = _studentRepo.GetStudent(selectedTaskProgress.StudentId);
            progress = selectedTaskProgress.Progress;
            description = selectedTaskProgress.Description;
            response = selectedTaskProgress.Response;
            updateAt = selectedTaskProgress.UpdateAt;
        }
    }
}
