using System.Windows.Input;
using System.Xml.Linq;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Shared;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.ViewModels
{
    public class TaskProgressVM : ViewModelBase
    {
        private readonly IStudentRepository _studentRepo;
        private readonly ITaskRepository _taskRepo;
        private readonly ITaskProgressRepository _taskProgressRepo;

        private int taskId;
        public int TaskId
        {
            get { return taskId; }
            set 
            { 
                taskId = value;
                OnPropertyChanged(nameof(TaskId)); 
            }
        }

        private Student? student;
        public Student? Student
        {
            get { return student; }
            set 
            { 
                student = value; 
                OnPropertyChanged(nameof(Student)); 
            }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
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

        private string progressFileName;
        public string ProgressFileName
        {
            get { return progressFileName; }
            set
            {
                progressFileName = value;
                OnPropertyChanged(nameof(ProgressFileName));
            }
        }

        private string response;
        public string Response
        {
            get { return response; }
            set
            {
                response = value;
                OnPropertyChanged(nameof(Response));
            }
        }

        public ICommand UpdateTaskProgressCommand { get; set; }

        public TaskProgressVM()
        {
            _taskRepo = new TaskRepository();
            _studentRepo = new StudentRepository(); 
            _taskProgressRepo = new TaskProgressRepository();
            if (SessionInfo.Role == Role.Student)
                Student = _studentRepo.GetStudent(SessionInfo.UserId);
            UpdateTaskProgressCommand = new ViewModelCommand(ExecuteUpdateTaskProgressCommand);
        }

        private void ExecuteUpdateTaskProgressCommand(object obj)
        {
            UpdateTaskProgressView taskProgressView = obj as UpdateTaskProgressView;
            if (SessionInfo.Role == Role.Student)
            {
                TaskProgress taskProgress = new TaskProgress
                {
                    Id = id,
                    TaskId = taskId,
                    StudentId = student.Id,
                    Progress = progress,
                    Description = description,
                    Response = response,
                    UpdateAt = DateTime.Now
                };
                if (id <= 0)
                {
                    var success = _taskProgressRepo.Add(taskProgress);
                    ShowMessage(success, Message.AddSuccess, Message.AddFailed);
                }
                else
                {
                    var success = _taskProgressRepo.Update(taskProgress);
                    ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
                }
            }         
            taskProgressView?.Close();
        }
    }
}
