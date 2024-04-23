using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ThesisManagement.Models;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.Repositories
{
    public interface ITaskRepository
    {
        bool Add(Task task);
        bool Update(Task task);
        bool Delete(int id);
        ObservableCollection<Task> GetAll();
        Task GetTask(int id);
        IEnumerable<Task> Get(int thesisId);
        IEnumerable<Task> GetPendingTasks(int thesisId);
        IEnumerable<Task> GetDoneTasks(int thesisId);
        IEnumerable<Task> GetOverdueTasks(int thesisId);
        IEnumerable<TasksPie> GetTasksPieData(int thesisId);
    }

    public class TaskRepository : BaseRepository, ITaskRepository
    {
        public TaskRepository() { }

        public bool Add(Task task)
        {
            _context.Add(task);
            return DbSave();
        }

        public bool Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return false;
            _context.Remove(task);
            return DbSave();
        }
        public bool Update(Task task)
        {
            _context.ChangeTracker.Clear();
            _context.Update(task);
            return DbSave();
        }

        public ObservableCollection<Task> GetAll()
        {
            var tasks = _context.Tasks.Include(th => th.Thesis).AsNoTracking().ToList();
            return new ObservableCollection<Task>(tasks);
        }

        public IEnumerable<Task> Get(int thesisId)
        {
            var tasks = _context.Tasks.Include(th => th.Thesis)
                                      .Where(t => t.ThesisId == thesisId)
                                      .AsNoTracking()
                                      .ToList();
            return tasks;
        }

        public IEnumerable<Task> GetPendingTasks(int thesisId)
        {
            var tasks = _context.Tasks.Include(th => th.Thesis)
                                      .Where(t => t.ThesisId == thesisId && t.Progress < 100 && t.End >= DateTime.Now)
                                      .AsNoTracking()
                                      .ToList();
            return tasks;
        }

        public IEnumerable<Task> GetDoneTasks(int thesisId)
        {
            var tasks = _context.Tasks.Include(th => th.Thesis)
                                      .Where(t => t.ThesisId == thesisId && t.Progress == 100)
                                      .AsNoTracking()
                                      .ToList();
            return tasks;
        }

        public IEnumerable<Task> GetOverdueTasks(int thesisId)
        {
            var tasks = _context.Tasks.Include(th => th.Thesis)
                                      .Where(t => t.ThesisId == thesisId && t.Progress < 100 && t.End < DateTime.Now)
                                      .AsNoTracking()
                                      .ToList();
            return tasks;
        }

        public IEnumerable<TasksPie> GetTasksPieData(int thesisId)
        {
            var data = new List<TasksPie>();
            var tasks = _context.Tasks.Include(th => th.Thesis)
                                      .Where(t => t.ThesisId == thesisId)
                                      .AsNoTracking();
            int totalTasks = tasks.Count();

            if (totalTasks > 0)
            {
                int countDone = tasks.Where(t => t.Progress == 100).Count();
                int countPending = tasks.Where(t => t.Progress < 100 && t.End >= DateTime.Now).Count();
                int countOverdue = tasks.Where(t => t.Progress < 100 && t.End < DateTime.Now).Count();
                data = new List<TasksPie>
                {
                    new TasksPie { TaskStatus = "Đã hoàn thành", Count = countDone, Percentage = (countDone/totalTasks)*100 },
                    new TasksPie { TaskStatus = "Đang thực hiện", Count = countPending, Percentage = (countPending/totalTasks)*100 },
                    new TasksPie { TaskStatus = "Đã quá hạn", Count = countOverdue, Percentage = (countOverdue/totalTasks)*100 }
                };
            }
            return data;
        }

        public Task GetTask(int id)
        {
            var task = _context.Tasks.Include(t => t.TaskProgresses)
                                    .ThenInclude(p => p.Student)
                                    .Include(t => t.TaskProgresses)
                                    .ThenInclude(p => p.Attachments)
                                    .FirstOrDefault(t => t.Id == id);
            return task;
        }
    }
}
