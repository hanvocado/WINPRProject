using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class ChartViewModel : ViewModelBase
    {
        private readonly ITaskRepository _taskRepository;

        private int thesisId;

        public int ThesisId
        {
            get { return thesisId; }
            set
            {
                thesisId = value;
                TasksPieData = _taskRepository.GetTasksPieData(ThesisId);
                OnPropertyChanged(nameof(ThesisId));
            }
        }

        private IEnumerable<TasksPie> tasksPieData;
        public IEnumerable<TasksPie> TasksPieData
        {
            get { return tasksPieData; }
            set { tasksPieData = value; OnPropertyChanged(nameof(TasksPieData)); }
        }

        public ChartViewModel()
        {
            _taskRepository = new TaskRepository();
        }
    }

    public class TasksPie
    {
        public string TaskStatus { get; set; }
        public int Count { get; set; }
        public int Percentage { get; set; }

    }
}
