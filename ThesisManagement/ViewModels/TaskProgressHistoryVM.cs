using ThesisManagement.Models;
using ThesisManagement.Repositories;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.ViewModels
{
    public class TaskProgressHistoryVM : ViewModelBase
    {
        private readonly ITaskRepository _taskRepo;

        private int taskId;

        public int TaskId
        {
            get { return taskId; }
            set
            {
                taskId = value;
                if (taskId > 0)
                    this.Task = _taskRepo.GetTask(taskId);
            }
        }

        private Task task;

        public Task Task
        {
            get { return task; }
            set { task = value; OnPropertyChanged(nameof(Task)); }
        }

        private IEnumerable<TaskProgress> progresses;

        public IEnumerable<TaskProgress> Progresses
        {
            get { return progresses; }
            set { progresses = value; OnPropertyChanged(nameof(Progresses)); }
        }

        public TaskProgressHistoryVM()
        {
            _taskRepo = new TaskRepository();
        }
    }
}
