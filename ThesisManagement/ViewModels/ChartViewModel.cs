namespace ThesisManagement.ViewModels
{
    public class ChartViewModel : ViewModelBase
    {
        private IEnumerable<TasksPie> tasksPieData;
        public IEnumerable<TasksPie> TasksPieData
        {
            get { return tasksPieData; }
            set { tasksPieData = value; OnPropertyChanged(nameof(TasksPieData)); }
        }
    }

    public class TasksPie
    {
        public TaskStatus TaskStatus { get; set; }
        public int Count { get; set; }
        public int Percentage { get; set; }
    }

    public enum TaskStatus
    {
        Done,
        Pending,
        Overdue
    }
}
