﻿using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ThesisManagement.Models;

namespace ThesisManagement.Repositories
{
    public interface IThesisRepository
    {
        bool Add(Thesis thesis);
        bool Add(Thesis thesis, IEnumerable<Student> students);
        bool Update(Thesis thesis);
        IEnumerable<Thesis> GetAll();
        IEnumerable<Thesis> Get(string userId, string topicStatus);
        IEnumerable<Thesis> Get(int topicId, string topicStatus);
        IEnumerable<Student> GetMembers(int thesisId);
        Thesis? GetThesis(int taskId);
        Thesis? Get(int thesisId);
    }

    public class ThesisRepository : BaseRepository, IThesisRepository
    {
        public ThesisRepository() { }

        public bool Add(Thesis thesis)
        {
            _context.Add(thesis);
            return DbSave();
        }

        public bool Add(Thesis thesis, IEnumerable<Student> students)
        {
            _context.Add(thesis);
            var isThesisAdded = DbSave();
            if (isThesisAdded)
            {
                _context.ChangeTracker.Clear();
                foreach (var student in students)
                {
                    var st = _context.Students.FirstOrDefault(s => s.Id == student.Id);
                    st.ThesisId = thesis.Id;
                    _context.Update(st);
                }
                return DbSave();
            }
            return false;
        }

        public bool Update(Thesis thesis)
        {
            _context.ChangeTracker.Clear();
            _context.Update(thesis);
            return DbSave();
        }

        public IEnumerable<Thesis> GetAll()
        {
            var theses = _context.Theses.Include(tp => tp.Topic).AsNoTracking().ToList();
            return new ObservableCollection<Thesis>(theses);
        }

        public IEnumerable<Thesis> Get(string userId, string topicStatus)
        {
            var list = _context.Theses.Include(th => th.Students).Include(th => th.Tasks).Include(th => th.Topic)
                                                .ThenInclude(pr => pr.Professor)
                                                .Where(th => th.Topic.ProfessorId == userId && th.TopicStatus == topicStatus).AsNoTracking().ToList();
            return list;
        }

        public IEnumerable<Thesis> Get(int topicId, string topicStatus)
        {
            var list = _context.Theses.Include(st => st.Students).Include(tp => tp.Topic)
                                    .ThenInclude(pr => pr.Professor)
                                    .Where(th => th.TopicId == topicId && th.TopicStatus == topicStatus).AsNoTracking().ToList();
            return list;
        }

        public IEnumerable<Student> GetMembers(int thesisId)
        {
            var students = _context.Theses.Include(st => st.Students).Where(th => th.Id == thesisId).SelectMany(th => th.Students).ToList();
            return students;
        }

        public Thesis? GetThesis(int taskId)
        {
            var task = _context.Tasks.Include(th => th.Thesis)
                                       .Where(t => t.Id == taskId)
                                       .AsNoTracking()
                                       .FirstOrDefault();
            return task?.Thesis;
        }

        public Thesis? Get(int thesisId)
        {
            var thesis = _context.Theses.Include(t => t.Topic).ThenInclude(t => t.Professor)
                        .AsNoTracking().FirstOrDefault(t => t.Id == thesisId);
            return thesis;
        }
    }
}
