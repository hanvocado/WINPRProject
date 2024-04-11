using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Task = ThesisManagement.Models.Task;

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

        private TaskProgress selectedTaskProgress;
        public TaskProgress SelectedTaskProgress
        {
            get { return selectedTaskProgress; }
            set { selectedTaskProgress = value; OnPropertyChanged(nameof(SelectedTaskProgress)); }
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
            selectedTaskProgress = new TaskProgress();

            UpdateTaskProgressCommand = new ViewModelCommand(ExecuteUpdateTaskProgressCommand);
        }

        private void ExecuteUpdateTaskProgressCommand(object obj)
        {
            UpdateTaskProgressView taskProgressView = obj as UpdateTaskProgressView;
            UpdateSelectedTaskProgressProperties();
            if (SessionInfo.Role == Role.Student)
            {
                if (id<=0)
                {
                    var success = _taskProgressRepo.Add(selectedTaskProgress);
                    ShowMessage(success, Message.AddSuccess, Message.AddFailed);
                }
                else
                {
                    var success = _taskProgressRepo.Update(selectedTaskProgress);
                    ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                }
            }
            else if (SessionInfo.Role == Role.Professor)
            {
                if (progress < 100)
                {
                    var success = _taskProgressRepo.Update(selectedTaskProgress);
                    ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                }    
                else
                {
                    var acceptedTask = _taskRepo.GetTask(taskId);
                    acceptedTask.Progress = progress;
                    var success = _taskRepo.Update(acceptedTask);
                    ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                }    
            }
            taskProgressView?.Close();
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
