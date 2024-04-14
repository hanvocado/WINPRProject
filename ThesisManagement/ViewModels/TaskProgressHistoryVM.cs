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

        public ViewModelCommand ShowUpdateTaskProgressView { get; set; }

        public TaskProgressHistoryVM()
        {
            _taskRepo = new TaskRepository();
            _studentRepo = new StudentRepository();
            _progressRepo = new TaskProgressRepository();
            ShowUpdateTaskProgressView = new ViewModelCommand(ExecuteShowUpdateTaskProgressView);
        }

        private void ExecuteShowUpdateTaskProgressView(object obj)
        {
            var vm = new TaskProgressVM();
            vm.TaskId = taskId;
            vm.UpdateAt = null;
            TaskProgress lastestTaskProgress = _progressRepo.GetLastestTaskProgress(taskId);
            if (lastestTaskProgress == null)
            {
                if (SessionInfo.Role == Role.Student)
                {
                    vm.Student = _studentRepo.GetStudent(SessionInfo.UserId);
                }
            }
            else
            {
                vm.SelectedTaskProgress = lastestTaskProgress;
                vm.UpdateLastestTaskProgress();
            }
            var updateView = new UpdateTaskProgressView { DataContext = vm };
            updateView.Show();
        }

        public void Reload()
        {
            this.Task = _taskRepo.GetTask(taskId);
            this.Progresses = task.TaskProgresses ?? new List<TaskProgress>();
        }
    }
}
