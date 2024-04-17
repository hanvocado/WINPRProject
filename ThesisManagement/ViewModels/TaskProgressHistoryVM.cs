using Syncfusion.XPS;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Shared;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.ViewModels
{
    public class TaskProgressHistoryVM : ViewModelBase
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IAttachmentRepository _attachmentRepo;
        private readonly ITaskProgressRepository _progressRepo;
        private readonly IStudentRepository _studentRepo;
        public TaskProgress lastestTaskProgress;

        private TasksVM? parentTasksVM;

        public TasksVM? ParentTasksVM
        {
            get { return parentTasksVM; }
            set { parentTasksVM = value; }
        }

        private int taskId;

        public int TaskId
        {
            get { return taskId; }
            set
            {
                taskId = value;
                if (taskId > 0)
                {
                    Reload();
                }
            }
        }

        private Task task;

        public Task Task
        {
            get { return task; }
            set { task = value; OnPropertyChanged(nameof(Task)); }
        }

        private string selectedFileName;

        public string SelectedFileName
        {
            get { return selectedFileName; }
            set
            {
                selectedFileName = value;
                OnPropertyChanged(nameof(SelectedFileName));
                if (!String.IsNullOrEmpty(value))
                    StartProcess(selectedFileName);
            }
        }

        private IEnumerable<TaskProgress> progresses;

        public IEnumerable<TaskProgress> Progresses
        {
            get { return progresses; }
            set { progresses = value; OnPropertyChanged(nameof(Progresses)); }
        }

        private ObservableCollection<Attachment> studentAttachments;
        public ObservableCollection<Attachment> StudentAttachments
        {
            get { return studentAttachments; }
            set
            {
                studentAttachments = value;
                OnPropertyChanged(nameof(StudentAttachments));
            }
        }

        private ObservableCollection<Attachment> professorAttachments;
        public ObservableCollection<Attachment> ProfessorAttachments
        {
            get { return professorAttachments; }
            set
            {
                professorAttachments = value;
                OnPropertyChanged(nameof(ProfessorAttachments));
            }
        }

        public ViewModelCommand ShowUpdateTaskProgressView { get; set; }

        public TaskProgressHistoryVM()
        {
            _taskRepo = new TaskRepository();
            _progressRepo = new TaskProgressRepository();
            _studentRepo = new StudentRepository();
            _progressRepo = new TaskProgressRepository();
            _attachmentRepo = new AttachmentRepository();
            ShowUpdateTaskProgressView = new ViewModelCommand(ExecuteShowUpdateTaskProgressView);
        }

        private void ExecuteShowUpdateTaskProgressView(object obj)
        {
            var professorResponse = _progressRepo?.GetLastestTaskProgress(taskId).Response;
            int numOfProgress = _progressRepo.CountTaskProgress(taskId);
            if(numOfProgress == 0 && SessionInfo.Role == Role.Professor)
            {
                MessageBox.Show("Giảng viên chưa thể phản hồi");
            }    
            else
            {
                if (numOfProgress > 0 && SessionInfo.Role == Role.Student && string.IsNullOrEmpty(professorResponse))
                {
                    MessageBox.Show("Đang đợi phàn hồi từ giảng viên hướng dẫn");
                    return;
                }
                else if (SessionInfo.Role == Role.Professor && !string.IsNullOrEmpty(professorResponse))
                {
                    MessageBox.Show("Đang đợi trả lời từ sinh viên");
                    return;
                }
                var vm = new TaskProgressVM();
                vm.TaskId = taskId;
                vm.SelectedTaskProgress = lastestTaskProgress;
                if (SessionInfo.Role == Role.Student)
                {
                    vm.Student = _studentRepo.GetStudent(SessionInfo.UserId);
                }
                var updateView = new UpdateTaskProgressView { DataContext = vm };
                updateView.Show();
            } 
        }

        public void Reload()
        {
            this.Task = _taskRepo.GetTask(taskId);
            this.Progresses = task.TaskProgresses ?? new List<TaskProgress>();
            if (lastestTaskProgress != null)
            {
                ProfessorAttachments = _attachmentRepo.GetAttachments(taskId, Role.Professor.ToString());
                StudentAttachments = _attachmentRepo.GetAttachments(taskId, Role.Student.ToString());
            }    
        }
    }
}
