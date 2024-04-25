using ThesisManagement.CustomControls;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Shared;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.ViewModels
{
    internal class TaskVM : ViewModelBase
    {
        private readonly IScheduleRepository _scheduleRepo;
        private readonly ITaskRepository _taskRepo;
        private readonly DialogService _dialogService;

        public TasksVM ParentVM { get; set; }

        private Task selectedTask;

        public Task SelectedTask
        {
            get { return selectedTask; }
            set { selectedTask = value; Load(); OnPropertyChanged(nameof(SelectedTask)); }
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
                selectedTask.ThesisId = value;
                OnPropertyChanged(nameof(ThesisId));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                selectedTask.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                selectedTask.Description = value;
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
                selectedTask.Start = value;
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
                selectedTask.End = value;
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
                selectedTask.WorkingTime = value;
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

        public ViewModelCommand CreateOrUpdateCommand { get; set; }

        public TaskVM()
        {
            _dialogService = new DialogService();
            _scheduleRepo = new ScheduleRepository();
            _taskRepo = new TaskRepository();
            CreateOrUpdateCommand = new ViewModelCommand(ExecuteCreateOrUpdateCommand);
            Load();
        }

        private void ExecuteCreateOrUpdateCommand(object obj)
        {
            ValidateInput();
            if (!existError)
            {
                var confirmed = _dialogService.ShowDialog(Message.Notification, Message.ContinueNotification);
                if (confirmed == true)
                {
                    TaskView? taskView = obj as TaskView;

                    ScheduleInfo schedule = new ScheduleInfo
                    {
                        Id = selectedTask?.ScheduleId ?? 0,
                        From = end.Date < start ? start : end.Date,
                        To = end,
                        EventName = name,
                        ThesisId = thesisId,
                        Thesis = null
                    };

                    if (id <= 0)
                    {
                        var success = _scheduleRepo.Add(schedule);
                        if (success)
                        {
                            selectedTask.ScheduleId = schedule.Id;
                            success = _taskRepo.Add(selectedTask);
                        }
                        ShowMessage(success, Message.AddSuccess, Message.AddFailed);
                    }
                    else
                    {
                        if (SessionInfo.Role == Role.Student)
                        {
                            ShowMessage(false, null, Message.StudentCant);
                            return;
                        }
                        var success = _taskRepo.Update(selectedTask);
                        if (success)
                            success = _scheduleRepo.Update(schedule);
                        ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                    }
                    ParentVM.Reload();
                    taskView?.Close();
                }
            }
        }

        private void ValidateInput()
        {
            ExistError = String.IsNullOrEmpty(name) || String.IsNullOrEmpty(description) || WorkingTime <= 0;
            if (existError)
                ShowMessage(false, null, Message.RequiredError);
        }

        private void UpdateWorkingTime()
        {
            float totalHours = Day * 24 + Hour + Minute / 60.0f;
            WorkingTime = totalHours;
        }

        public void Load()
        {
            if (selectedTask != null)
            {
                Id = selectedTask.Id;
                ThesisId = selectedTask.ThesisId;
                Name = selectedTask.Name;
                Description = selectedTask.Description;
                Start = selectedTask.Start;
                End = selectedTask.End;
                var workingTime = selectedTask.WorkingTime;
                Day = (int)(workingTime / 24);
                Hour = (int)(workingTime % 24);
                Minute = (int)((workingTime - (int)workingTime) * 60 + 0.5f);
            }
            else
            {
                selectedTask = new Task();
                Start = DateTime.Now;
                End = DateTime.Now.AddDays(7);
            }
        }
    }
}
