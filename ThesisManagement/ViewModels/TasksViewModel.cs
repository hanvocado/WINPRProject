using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Professor;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.ViewModels
{
    public class TasksViewModel : ViewModelBase
    {
        private readonly ITaskRepository _taskRepo;

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

        private Thesis thesis;
        public Thesis Thesis
        {
            get { return thesis; }
            set
            {
                thesis = value;
                OnPropertyChanged(nameof(Thesis));
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

        private IEnumerable<Task> tasks;
        public IEnumerable<Task> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }

        public ICommand CreateTaskCommand { get; set; }
        public ICommand CreateOrUpdateCommand { get; set; }

        public TasksViewModel()
        {
            _taskRepo = new TaskRepository();
            CreateTaskCommand = new ViewModelCommand(ExecuteCreateTaskCommand);
            CreateOrUpdateCommand = new ViewModelCommand(ExecuteCreateOrUpdateCommand);
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
            Trace.WriteLine($"Selected Thesis nhận được là {thesisId}");
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

            Tasks = _taskRepo.Get(thesisId);

            taskView?.Close();

            var mainWindow = Application.Current.MainWindow;
            mainWindow.Focus();
        }

        private void ExecuteCreateTaskCommand(object obj)
        {
            TaskView taskView = new();
            taskView.DataContext = this;
            taskView.Owner = Application.Current.MainWindow;
            taskView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            taskView.Show();
        }

    }
}
