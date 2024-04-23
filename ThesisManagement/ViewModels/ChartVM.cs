using ThesisManagement.Models;
using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class ChartVM : ViewModelBase
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IThesisRepository _thesisRepo;
        private readonly ITaskRepository _taskRepo;

        private int thesisId;

        public int ThesisId
        {
            get { return thesisId; }
            set { thesisId = value; Reload(); }
        }

        private List<TasksPie> tasksPieData;
        public List<TasksPie> TasksPieData
        {
            get { return tasksPieData; }
            set { tasksPieData = value; OnPropertyChanged(nameof(TasksPieData)); }
        }

        private IEnumerable<ThesesChartData> thesesData;
        public IEnumerable<ThesesChartData> ThesesData
        {
            get { return thesesData; }
            set { thesesData = value; OnPropertyChanged(nameof(ThesesData)); }
        }

        private IEnumerable<Student> students;

        public IEnumerable<Student> Students
        {
            get { return students; }
            set { students = value; OnPropertyChanged(nameof(Students)); }
        }

        public ChartVM()
        {
            _thesisRepo = new ThesisRepository();
            _taskRepo = new TaskRepository();
        }

        public void Reload()
        {
            if (thesisId != 0)
            {
                TasksPieData = _taskRepo.GetTasksPieData(thesisId).ToList();
                ThesesData = _thesisRepo.CompareThesesData(SessionInfo.UserId);
                Students = _thesisRepo.GetMembers(thesisId);
            }
        }
    }

    public class ThesesChartData
    {
        public string Students { get; set; }
        public float TotalTaskTime { get; set; }
        public float WorkedTime { get; set; }

        public ThesesChartData(string students, float totalTaskTime, float workedTime)
        {
            Students = students;
            TotalTaskTime = totalTaskTime;
            WorkedTime = workedTime;
        }
    }
}
