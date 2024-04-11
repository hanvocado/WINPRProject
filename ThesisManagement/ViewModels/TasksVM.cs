using System.Windows;
using System.Windows.Input;
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
            set { selectedTask = value; OnPropertyChanged(nameof(SelectedTask)); }
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

        public string DeleteBtnVisibility
        {
            get
            {
                if (SessionInfo.Role == Role.Student)
                    return "Hidden";
                else
                    return "Visible";
            }
        }

        private IEnumerable<TasksPie> tasksPieData;
        public IEnumerable<TasksPie> TasksPieData
        {
            get { return tasksPieData; }
            set { tasksPieData = value; OnPropertyChanged(nameof(TasksPieData)); }
        }

        public ICommand CreateTaskCommand { get; set; }
        public ICommand UpdateTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }
        public ICommand CreateOrUpdateCommand { get; set; }
        public ICommand ShowUpdateProgressCommand { get; set; }

        public TasksVM()
        {
            _taskRepo = new TaskRepository();
            _studentRepo = new StudentRepository();
            _taskProgressRepo = new TaskProgressRepository();
            Thesis = new Thesis();

            CreateTaskCommand = new ViewModelCommand(ExecuteCreateTaskCommand, CanExecuteCreateTask);
            UpdateTaskCommand = new ViewModelCommand(ExecuteUpdateTaskCommand);
            DeleteTaskCommand = new ViewModelCommand(ExecuteDeleteTaskCommand, CanExecuteDeleteTask);
            CreateOrUpdateCommand = new ViewModelCommand(ExecuteCreateOrUpdateCommand);
            ShowUpdateProgressCommand = new ViewModelCommand(ExecuteShowUpdateProgressCommand);
        }

        private void ExecuteShowUpdateProgressCommand(object obj)
        {
            var vm = new TaskProgressVM();
            vm.TaskId = id;
            TaskProgress lastestTaskProgress = _taskProgressRepo.GetLastestTaskProgress(id);
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
            updateView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            updateView.Show();
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
            var success = _taskRepo.Delete(id);
            ShowMessage(success, Message.DeleteSuccess, Message.DeleteFailed);
            Reload();
        }

        private void ExecuteCreateOrUpdateCommand(object obj)
        {
            //if (IsTaskNotValid())
            //    return;

            TaskView taskView = obj as TaskView;
            Task task = new Task
            {
                Id = id,
                ThesisId = thesisId,
                Name = name,
                Description = description,
                Start = start,
                End = end,
                Progress = progress
            };
            if (id <= 0)
            {
                var success = _taskRepo.Add(task);
                ShowMessage(success, Message.AddSuccess, Message.AddFailed);
            }
            else
            {
                var success = _taskRepo.Update(task);
                ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
            }

            Reload();
            taskView?.Close();
        }

        private void ExecuteCreateTaskCommand(object obj)
        {
            ResetTaskProperties();
            var taskView = new TaskView { DataContext = this };
            taskView.Show();
        }

        private void Reload()
        {
            PendingTasks = _taskRepo.GetPendingTasks(thesis.Id);
            DoneTasks = _taskRepo.GetDoneTasks(thesis.Id);
            OverdueTasks = _taskRepo.GetOverdueTasks(thesis.Id);
            TasksPieData = _taskRepo.GetTasksPieData(thesis.Id);
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
    }
}
