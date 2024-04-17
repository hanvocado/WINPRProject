﻿using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface ITaskProgressRepository
    {
        bool Add(TaskProgress taskProgress);
        bool Update(TaskProgress taskProgress);
        bool Update(int progressId, string response);
        bool Delete(int id);
        ObservableCollection<TaskProgress> GetAll();
        TaskProgress GetLastestTaskProgress(int taskId);
        int CountTaskProgress(int taskId);
    }

    public class TaskProgressRepository : ITaskProgressRepository
    {
        private AppDbContext _context;
        private TaskProgress? taskProgress;
        public TaskProgressRepository()
        {
            _context = DataProvider.Instance.Context;
        }
        public bool Add(TaskProgress taskProgress)
        {
            _context.Add(taskProgress);
            return DbSave();
        }

        public bool Delete(int id)
        {
            taskProgress = _context.TaskProgresses.FirstOrDefault(tp => tp.Id == id);
            if (taskProgress == null) return false;
            _context.Remove(taskProgress);
            return DbSave();
        }
        public bool Update(TaskProgress taskProgress)
        {
            _context.ChangeTracker.Clear();
            _context.Update(taskProgress);
            return DbSave();
        }

        public bool DbSave()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Trace.WriteLine(ex);
                return false;
            }
        }

        public ObservableCollection<TaskProgress> GetAll()
        {
            var taskProgresses = _context.TaskProgresses.Include(t => t.Task).AsNoTracking().ToList();
            return new ObservableCollection<TaskProgress>(taskProgresses);
        }

        public TaskProgress GetLastestTaskProgress(int taskId)
        {
            var lastestTaskProgress = _context.TaskProgresses.Include(t => t.Task)
                                                             .Include(t => t.Student)
                                                             .Where(tp => tp.TaskId == taskId)
                                                             .AsNoTracking()
                                                             .OrderByDescending(tp => tp.Id)
                                                             .FirstOrDefault();
            return lastestTaskProgress;
        }

        public int CountTaskProgress(int taskId)
        {
            int count = _context.TaskProgresses.Include(t => t.Task)
                                               .Where(tp => tp.TaskId == taskId)
                                               .AsNoTracking()
                                               .Count();
            return count;
        }

        public bool Update(int progressId, string response)
        {
            var progress = _context.TaskProgresses.FirstOrDefault(t => t.Id == progressId);
            if (progress != null)
            {
                progress.Response = response;
                _context.Update(progress);
                return DbSave();
            }
            return false;
        }
    }
}
