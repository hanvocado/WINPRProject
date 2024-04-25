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
                Reload();
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
        private TaskProgress? lastestProgress;

        private Visibility updateBtnVisibility = Visibility.Visible;
        public Visibility UpdateBtnVisibility
        {
            get
            {
                return updateBtnVisibility;
            }
            set
            {
                updateBtnVisibility = value;
                OnPropertyChanged(nameof(UpdateBtnVisibility));
            }
        }

        public ViewModelCommand ShowUpdateTaskProgressView { get; set; }

        public TaskProgressHistoryVM()
        {
            _taskRepo = new TaskRepository();
            _progressRepo = new TaskProgressRepository();
            _studentRepo = new StudentRepository();
            _progressRepo = new TaskProgressRepository();
            ShowUpdateTaskProgressView = new ViewModelCommand(ExecuteShowUpdateTaskProgressView);
        }

        private void ExecuteShowUpdateTaskProgressView(object obj)
        {
            if (SessionInfo.Role == Role.Professor)
            {
                ShowLastestProgress();
            }
            else
            {
                ShowNewProgressView();
            }
        }

        public void Reload()
        {
            if (taskId > 0) 
            {
                this.Task = _taskRepo.GetTask(taskId);
                this.lastestProgress = _progressRepo.GetLastestTaskProgress(taskId);
                this.Progresses = task?.TaskProgresses?.OrderBy(tp => tp.Id).ToList() ?? new List<TaskProgress>();
                if (SessionInfo.Role == Role.Student && lastestProgress != null && lastestProgress.Response == null)
                    UpdateBtnVisibility = Visibility.Collapsed;
                else if (SessionInfo.Role == Role.Professor && lastestProgress?.Response != null)
                    UpdateBtnVisibility = Visibility.Collapsed;
            }
        }

        private void ShowLastestProgress()
        {
            var vm = new TaskProgressVM
            {
                ParentVM = this,
                TaskId = lastestProgress.TaskId,
                Id = lastestProgress.Id,
                UpdateAt = lastestProgress?.UpdateAt,
                Description = lastestProgress.Description,
                Response = lastestProgress.Response ?? "ok",
                StudentId = lastestProgress.StudentId,
                Progress = lastestProgress.Progress,
            };
            var view = new UpdateTaskProgressView { DataContext = vm };
            view.Show();
        }

        private void ShowNewProgressView()
        {
            var vm = new TaskProgressVM
            {
                ParentVM = this,
                TaskId = taskId,
                Progress = lastestProgress?.Progress ?? 0,
                StudentId = SessionInfo.UserId
            };

            var view = new UpdateTaskProgressView { DataContext = vm };
            view.Show();
        }
    }
}
