using static ThesisManagement.Helpers.Variable;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using static ThesisManagement.Helpers.Query;
using System.Diagnostics;
using System.Windows;
using System.Linq.Expressions;

namespace ThesisManagement.Repositories
{
    public interface IThesisRepository
    {
        bool Update(Thesis thesis);
        IEnumerable<Thesis> GetAll();
        IEnumerable<Thesis> Get(string topicStatus);
        IEnumerable<Thesis> Get(int topicId, string topicStatus);
        IEnumerable<Student> GetMembers(int thesisId);
        bool CanRegisterTopic(int thesisId);

        //void AddMembers(int topicId, IEnumerable<Student> members);
    }
    public class ThesisRepository : IThesisRepository
    {
        private AppDbContext _context;

        public ThesisRepository()
        {
            _context = DataProvider.Instance.Context;
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

        public IEnumerable<Thesis> Get(string topicStatus)
        {
            var list = _context.Theses.Include(st => st.Students).Include(tp => tp.Topic)
                                                .ThenInclude(pr => pr.Professor)
                                                .Where(th => th.TopicStatus == topicStatus).AsNoTracking().ToList();
            return list;
        }

        public IEnumerable<Thesis> Get(int topicId, string topicStatus)
        {
            var list = _context.Theses.Include(st => st.Students).Include(tp => tp.Topic)
                                    .ThenInclude(pr => pr.Professor)
                                    .Where(th => th.TopicId == topicId && th.TopicStatus == topicStatus).AsNoTracking().ToList();
            return list;
        }

        public bool CanRegisterTopic(int thesisId)
        {
            var currentStudents = GetMembers(thesisId);
            int currentQuantity = currentStudents.Count();
            bool waiting = _context.Theses.Include(tp => tp.Topic)
                                          .Where(th => th.Id == thesisId && th.TopicStatus == Variable.StatusTopic.Waiting)
                                          .Any(th => th.Topic.StudentQuantity > currentQuantity);
            return waiting;
        }

        public IEnumerable<Student> GetMembers(int thesisId)
        {
            var students = _context.Theses.Include(st => st.Students).Where(th => th.Id == thesisId).SelectMany(th => th.Students).ToList();
            return students;
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

        //public void AddMembers(int topicId, IEnumerable<Student> members)
        //{
        //    var studentTopics = new List<StudentTopic>();
        //    foreach (Student member in members)
        //    {
        //        studentTopics.Add(new StudentTopic() { StudentId = member.Id, TopicId = topicId, Status = Variable.StudentTopic.Waiting });
        //    }
        //    _context.StudentTopics.AddRange(studentTopics);
        //}

    }
}
