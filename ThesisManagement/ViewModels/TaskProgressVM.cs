using System.Diagnostics;
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
        private readonly ITaskRepository _taskRepo;
        private readonly ITaskProgressRepository _taskProgressRepo;

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

        private string progressFileName;
        public string ProgressFileName
        {
            get { return progressFileName; }
            set
            {
                progressFileName = value;
                OnPropertyChanged(nameof(ProgressFileName));
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

        private IEnumerable<Attachment> attachments;

        public IEnumerable<Attachment> Attachments
        {
            get { return attachments; }
            set { attachments = value; OnPropertyChanged(nameof(Attachments)); }
        }


        private string appDirectory;

        private Attachment selectedFile;
        public Attachment SelectedFile
        {
            get { return selectedFile; }
            set
            {
                if (!String.IsNullOrEmpty(value.FileName) && value != selectedFile)
                {
                    selectedFile = value;
                    OnPropertyChanged(nameof(SelectedFile));
                    StartDownload(selectedFile);
                }
            }
        }

        public ICommand UpdateTaskProgressCommand { get; set; }

        public TaskProgressVM()
        {
            _taskRepo = new TaskRepository();
            _studentRepo = new StudentRepository();
            _taskProgressRepo = new TaskProgressRepository();
            appDirectory = Directory.GetCurrentDirectory();
            if (SessionInfo.Role == Role.Student)
                Student = _studentRepo.GetStudent(SessionInfo.UserId);
            UpdateTaskProgressCommand = new ViewModelCommand(ExecuteUpdateTaskProgressCommand);
        }

        private void ExecuteUpdateTaskProgressCommand(object obj)
        {
            UpdateTaskProgressView taskProgressView = obj as UpdateTaskProgressView;
            if (SessionInfo.Role == Role.Student)
            {
                TaskProgress taskProgress = new TaskProgress
                {
                    Id = id,
                    TaskId = taskId,
                    StudentId = student.Id,
                    Progress = progress,
                    Description = description,
                    Response = response,
                    UpdateAt = DateTime.Now
                };
                if (id <= 0)
                {
                    var success = _taskProgressRepo.Add(taskProgress);
                    ShowMessage(success, Message.AddSuccess, Message.AddFailed);
                }
                else
                {
                    var success = _taskProgressRepo.Update(taskProgress);
                    ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                }
            }
            taskProgressView?.Close();
        }

        private void StartDownload(Attachment selectedFile)
        {
            string filePath = Path.Combine(appDirectory, "UserFile", selectedFile.FileName);
            if (File.Exists(filePath))
            {
                try
                {
                    Process.Start(filePath);
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
