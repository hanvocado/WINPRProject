using Microsoft.Win32;
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
        private readonly IAttachmentRepository _attachmentRepo;
        private readonly ITaskRepository _taskRepo;
        private readonly IThesisRepository _thesisRepo;
        private readonly ITaskProgressRepository _taskProgressRepo;
        private TasksVM taskVM;
        private Attachment attachment;
        public string appDirectory;
        public string destinationPath;

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
            set
            {
                selectedTaskProgress = value;
                OnPropertyChanged(nameof(SelectedTaskProgress));
                LoadTaskProgress();
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

        private Stream docStream;
        public Stream DocumentStream
        {
            get
            {
                return docStream;
            }
            set
            {
                docStream = value;
                OnPropertyChanged(nameof(DocumentStream));
            }
        }

        private string attachedFile;
        public string AttachedFile
        {
            get { return attachedFile; }
            set
            {
                attachedFile = value;
                OnPropertyChanged(nameof(AttachedFile));
            }
        }

        private IEnumerable<Attachment> attachments;
        public IEnumerable<Attachment> Attachments
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
        public ICommand TestDownloadFile { get; set; }

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
            TestDownloadFile = new ViewModelCommand(ExecuteTestDownload);
            UploadAttachmentCommand = new ViewModelCommand(ExecuteUploadAttachmentCommand);
        }

        private void LoadTaskProgress()
        {
            //Attachments = _attachmentRepo.GetAttachments(selectedTaskProgress.Id);

            //if (selectedTaskProgress != null && !string.IsNullOrEmpty(selectedTaskProgress.Attachments))
            //{
            //    //Update professor evaluation
            //    Evaluation = thesis.Evaluation;
            //    Score = thesis.Score;

            //    //Current file path

            //    var attachmentPath = Path.Combine(appDirectory, attachment.AttachedFile);
            //    docStream = new FileStream(attachmentPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            //}
        }

        private void ExecuteUploadAttachmentCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                //Uploaded attachment name
                string attachmentName = openFileDialog.FileName;
                string userAttachmentName = SessionInfo.UserId + Path.GetFileName(attachmentName);

                //Storage attachment name
                destinationPath = Path.Combine(appDirectory, userAttachmentName);
                File.Copy(attachmentName, destinationPath, true);
                DocumentStream = new FileStream(destinationPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);

                //Update database
                attachment.FileName = userAttachmentName;
                attachment.TaskProgressId = selectedTaskProgress.Id;
                var success = _attachmentRepo.Add(attachment);
                ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
            }
        }

        private void ExecuteUpdateTaskProgressCommand(object obj)
        {
            UpdateTaskProgressView taskProgressView = obj as UpdateTaskProgressView;
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
            string filePath = Path.Combine(appDirectory, selectedFile.FileName);
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

        public void ExecuteTestDownload(object obj)
        {
            string filePath = Path.Combine(appDirectory, "22133010Bài tập thiết kế CSDL.pdf");
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
