using Microsoft.EntityFrameworkCore;
using ThesisManagement.Models;

namespace ThesisManagement.Repositories
{
    public interface ITaskProgressRepository
    {
        bool Add(TaskProgress taskProgress);
        bool Update(int progressId, string response, int progress);
        TaskProgress? GetLastestTaskProgress(int taskId);
    }

    public class TaskProgressRepository : BaseRepository, ITaskProgressRepository
    {
        public TaskProgressRepository() { }

        public bool Add(TaskProgress taskProgress)
        {
            _context.Add(taskProgress);
            return DbSave();
        }

        public TaskProgress? GetLastestTaskProgress(int taskId)
        {
            var lastestTaskProgress = _context.TaskProgresses.Include(t => t.Task)
                                                             .Include(t => t.Student)
                                                             .Where(tp => tp.TaskId == taskId)
                                                             .AsNoTracking()
                                                             .OrderByDescending(tp => tp.Id)
                                                             .FirstOrDefault();
            return lastestTaskProgress;
        }

        public bool Update(int progressId, string response, int progress)
        {
            var taskProgress = _context.TaskProgresses.FirstOrDefault(t => t.Id == progressId);
            if (taskProgress != null)
            {
                taskProgress.Progress = progress;
                taskProgress.Response = response;
                _context.Update(taskProgress);
                return DbSave();
            }
            return false;
        }
    }
}
