using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using ThesisManagement.CustomControls;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Shared;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.ViewModels
{
    public class TasksVM : ViewModelBase
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly ITaskProgressRepository _taskProgressRepo;
        private readonly IScheduleRepository _scheduleRepo;
        private readonly DialogService _dialogService;

        private Thesis thesis;
        public Thesis Thesis
        {
            get { return thesis; }
            set
            {
                thesis = value;
                Reload();
                OnPropertyChanged(nameof(Thesis));
            }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private int thesisId;

        public int ThesisId
        {
            get { return thesisId; }
            set
            {
                thesisId = value;
                OnPropertyChanged(nameof(ThesisId));
            }
        }

        private string name;
        [Required]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string description;
        [Required]
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private DateTime start;
        public DateTime Start
        {
            get { return start; }
            set
            {
                start = value;
                OnPropertyChanged(nameof(Start));
            }
        }

        private DateTime end;
        public DateTime End
        {
            get { return end; }
            set
            {
                end = value;
                OnPropertyChanged(nameof(End));
            }
        }
        
        private float workingTime;
        public float WorkingTime
        {
            get { return workingTime; }
            set
            {
                workingTime = value;
                OnPropertyChanged(nameof(WorkingTime));
            }
        }

        private int day;
        public int Day
        {
            get { return day; }
            set
            {
                if (day != value)
                {
                    day = value;
                    OnPropertyChanged(nameof(Day));
                    UpdateWorkingTime();
                }
            }
        }

        private int hour;
        public int Hour
        {
            get { return hour; }
            set
            {
                if (hour != value)
                {
                    hour = value;
                    OnPropertyChanged(nameof(Hour));
                    UpdateWorkingTime();
                }
            }
        }

        private int minute;
        public int Minute
        {
            get { return minute; }
            set
            {
                if (minute != value)
                {
                    minute = value;
                    OnPropertyChanged(nameof(Minute));
                    UpdateWorkingTime();
                }
            }
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

        private Task? selectedTask;
        public Task? SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
                if (value != null)
                {
                    ShowSelectedTaskView();
                }
            }
        }

        private IEnumerable<Task> pendingTasks;
        public IEnumerable<Task> PendingTasks
        {
            get { return pendingTasks; }
            set
            {
                pendingTasks = value;
                OnPropertyChanged(nameof(PendingTasks));
            }
        }

        private IEnumerable<Task> doneTasks;
        public IEnumerable<Task> DoneTasks
        {
            get { return doneTasks; }
            set
            {
                doneTasks = value;
                OnPropertyChanged(nameof(DoneTasks));
            }
        }

        private IEnumerable<Task> overdueTasks;
        public IEnumerable<Task> OverdueTasks
        {
            get { return overdueTasks; }
            set
            {
                overdueTasks = value;
                OnPropertyChanged(nameof(OverdueTasks));
            }
        }

        private IEnumerable<Task> undoneTasks;
        public IEnumerable<Task> UndoneTasks
        {
            get { return undoneTasks; }
            set
            {
                undoneTasks = value;
                OnPropertyChanged(nameof(UndoneTasks));
            }
        }

        private IEnumerable<TasksPie> tasksPieData;
        public IEnumerable<TasksPie> TasksPieData
        {
            get { return tasksPieData; }
            set { tasksPieData = value; OnPropertyChanged(nameof(TasksPieData)); }
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

        public ViewModelCommand CreateTaskCommand { get; set; }
        public ViewModelCommand UpdateTaskCommand { get; set; }
        public ViewModelCommand DeleteTaskCommand { get; set; }
        public ViewModelCommand CreateOrUpdateCommand { get; set; }
        public ViewModelCommand ShowTaskProgressHistory { get; set; }

        public TasksVM()
        {
            _taskRepo = new TaskRepository();
            _studentRepo = new StudentRepository();
            _taskProgressRepo = new TaskProgressRepository();
            _scheduleRepo = new ScheduleRepository();
            _dialogService = new DialogService();
            Thesis = new Thesis();

            CreateTaskCommand = new ViewModelCommand(ExecuteCreateTaskCommand, CanExecuteCreateTask);
            UpdateTaskCommand = new ViewModelCommand(ExecuteUpdateTaskCommand);
            DeleteTaskCommand = new ViewModelCommand(ExecuteDeleteTaskCommand, CanExecuteDeleteTask);
            CreateOrUpdateCommand = new ViewModelCommand(ExecuteCreateOrUpdateCommand);
            ShowTaskProgressHistory = new ViewModelCommand(ExecuteShowTaskHistory);
        }

        private void ExecuteShowTaskHistory(object obj)
        {
            var viewModel = new TaskProgressHistoryVM
            {
                ParentTasksVM = this,
                TaskId = (int)obj
            };
            var view = new TaskProgressHistoryView { DataContext = viewModel };
            view.Show();
        }

        private bool CanExecuteCreateTask(object obj)
        {
            return thesis.TopicStatus == Variable.StatusTopic.Approved;
        }

        private bool CanExecuteDeleteTask(object obj)
        {
            return SessionInfo.Role != Role.Student;
        }

        private void ExecuteUpdateTaskCommand(object obj)
        {
            TaskView taskView = new();
            taskView.DataContext = this;
            taskView.Show();
        }

        private void ExecuteDeleteTaskCommand(object obj)
        {
            bool? confirmDelete = _dialogService.ShowDialog(Message.Notification, Message.DeleteTaskNotification);
            if (confirmDelete == true)
            {
                var success = _taskRepo.Delete(id);
                ShowMessage(success, Message.DeleteSuccess, Message.DeleteFailed);
                Reload();
            }
        }

        private void ExecuteCreateOrUpdateCommand(object obj)
        {
            ValidateInput();
            if (!existError)
            {
                TaskView taskView = obj as TaskView;
                Task task = new Task
                {
                    Id = id,
                    ThesisId = thesisId,
                    Name = name,
                    Description = description,
                    Start = start,
                    End = end,
                    WorkingTime = workingTime,
                    Progress = progress
                };

                ScheduleInfo schedule = new ScheduleInfo
                {
                    From = end.AddHours(-1),
                    To = end,
                    EventName = name,
                    ThesisId = thesisId
                };

                if (id <= 0)
                {
                    bool? confirmAdd = _dialogService.ShowDialog(Message.Notification, Message.AddTaskNotification);
                    if (confirmAdd == true)
                    {
                        var success = _taskRepo.Add(task);
                        _scheduleRepo.Add(schedule);
                        ShowMessage(success, Message.AddSuccess, Message.AddFailed);
                    }
                }
                else
                {
                    bool? confirmUpdate = _dialogService.ShowDialog(Message.Notification, Message.UpdateTaskNotification);
                    if (confirmUpdate == true)
                    {
                        var success = _taskRepo.Update(task);
                        _scheduleRepo.Update(schedule);
                        ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                    }
                }

                Reload();
                taskView?.Close();
            }
        }

        private void ExecuteCreateTaskCommand(object obj)
        {
            ResetTaskProperties();
            var taskView = new TaskView { DataContext = this };
            taskView.Show();
        }

        private void ShowSelectedTaskView()
        {
            TaskView taskView = new TaskView();

            Id = selectedTask.Id;
            ThesisId = selectedTask.ThesisId;
            Name = selectedTask.Name;
            Description = selectedTask.Description;
            Start = selectedTask.Start;
            End = selectedTask.End;
            Progress = selectedTask.Progress;
            var workingTime = selectedTask.WorkingTime;
            Day = (int)(workingTime / 24);
            Hour = (int)(workingTime % 24);
            Minute = (int)((workingTime - (int)workingTime) * 60 + 0.5f); 

            taskView.DataContext = this;
            taskView.Show();
            SelectedTask = null;
        }

        public void Reload()
        {
            PendingTasks = _taskRepo.GetPendingTasks(thesis.Id);
            DoneTasks = _taskRepo.GetDoneTasks(thesis.Id);
            OverdueTasks = _taskRepo.GetOverdueTasks(thesis.Id);
            TasksPieData = _taskRepo.GetTasksPieData(thesis.Id);
            UndoneTasks = _taskRepo.GetUndoneTasks(thesis.Id);
        }

        private void ResetTaskProperties()
        {
            Id = 0;
            Name = "";
            Description = "";
            Start = DateTime.Now;
            End = DateTime.Now.AddDays(7);
            Progress = 0;
        }

        private void ValidateInput()
        {
            ExistError = String.IsNullOrEmpty(name) || String.IsNullOrEmpty(description);
            if (existError)
                ShowMessage(false, null, Message.RequiredError);
        }

        private void UpdateWorkingTime()
        {
            float totalHours = Day * 24 + Hour + Minute / 60.0f;
            WorkingTime = totalHours;
        }
    }
}
