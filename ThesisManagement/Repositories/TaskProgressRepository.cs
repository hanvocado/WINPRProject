﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface ITaskProgressRepository
    {
        bool Add(TaskProgress taskProgress);
        bool Update(TaskProgress taskProgress);
        bool Delete(int id);
        ObservableCollection<TaskProgress> GetAll();
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
    }
}
