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
        private readonly IThesisRepository _thesisRepo;
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

        private IEnumerable<Task> tasks;

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

        private int totalTasks;

        public int TotalTasks
        {
            get { return totalTasks; }
            set { totalTasks = value; OnPropertyChanged(nameof(TotalTasks)); }
        }

        private int waitingForReponse;

        public int WaitingForResponse
        {
            get { return waitingForReponse; }
            set { waitingForReponse = value; OnPropertyChanged(nameof(WaitingForResponse)); }
        }

        public ChartVM? ChartViewModel;
        public ScheduleVM? ScheduleViewModel;

        public ViewModelCommand CreateTaskCommand { get; set; }
        public ViewModelCommand UpdateTaskCommand { get; set; }
        public ViewModelCommand DeleteTaskCommand { get; set; }
        public ViewModelCommand ShowTaskProgressHistory { get; set; }

        public TasksVM()
        {
            _taskRepo = new TaskRepository();
            _dialogService = new DialogService();
            _thesisRepo = new ThesisRepository();
            CreateTaskCommand = new ViewModelCommand(ExecuteCreateTaskCommand);
            UpdateTaskCommand = new ViewModelCommand(ExecuteUpdateTaskCommand);
            DeleteTaskCommand = new ViewModelCommand(ExecuteDeleteTaskCommand, CanExecuteDeleteTask);
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

        private bool CanExecuteDeleteTask(object obj)
        {
            return IsProfessor;
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
                var success = _taskRepo.Delete((int)obj);
                ShowMessage(success, Message.DeleteSuccess, Message.DeleteFailed);
                Reload();
            }
        }

        private void ExecuteCreateTaskCommand(object obj)
        {
            var taskView = new TaskView { DataContext = new TaskVM { ThesisId = thesis.Id, ParentVM = this } };
            taskView.Show();
        }

        private void ShowSelectedTaskView()
        {
            var taskViewModel = new TaskVM
            {
                SelectedTask = selectedTask,
                ThesisId = thesis.Id,
                ParentVM = this
            };
            TaskView taskView = new TaskView { DataContext = taskViewModel };
            taskView.Show();
            SelectedTask = null;
        }

        public void Reload()
        {
            if (thesis != null)
            {
                tasks = _taskRepo.GetAll(thesis.Id);
                PendingTasks = tasks.Where(t => t.Progress < 100 && t.End >= DateTime.Now).OrderByDescending(t => t.HasNewUpdate);
                DoneTasks = tasks.Where(t => t.Progress == 100);
                OverdueTasks = tasks.Where(t => t.Progress < 100 && t.End < DateTime.Now);
                TotalTasks = tasks.Count();
                thesis = _thesisRepo.Get(thesis.Id);
                WaitingForResponse = thesis?.WaitingForResponse ?? 0;
                ChartViewModel?.Reload();
                ScheduleViewModel?.Load();
            }
        }
    }
}
