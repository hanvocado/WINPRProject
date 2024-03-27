using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.Repositories
{
    public interface ITaskRepository
    {
        bool Add(Task task);
        bool Update(Task task);
        bool Delete(int id);
        ObservableCollection<Task> GetAll();
        IEnumerable<Task> Get(int thesisId);
    }

    public class TaskRepository : ITaskRepository
    {
        private AppDbContext _context;
        private Task? task;
        public TaskRepository()
        {
            _context = DataProvider.Instance.Context;
        }
        public bool Add(Task task)
        {
            _context.Add(task);
            return DbSave();
        }

        public bool Delete(int id)
        {
            task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return false;
            _context.Remove(task);
            return DbSave();
        }
        public bool Update(Task task)
        {
            _context.Update(task);
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

        public ObservableCollection<Task> GetAll()
        {
            var tasks = _context.Tasks.Include(th => th.Thesis).AsNoTracking().ToList();
            return new ObservableCollection<Task>(tasks);
        }

        public IEnumerable<Task> Get(int thesisId)
        {
            var tasks = _context.Tasks.Include(th => th.Thesis)
                                      .Where(t => t.ThesisId == thesisId)
                                      .ToList();
            return tasks;
        }
    }
}
