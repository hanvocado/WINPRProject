using static ThesisManagement.Helpers.Variable;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;
using Microsoft.EntityFrameworkCore;

namespace ThesisManagement.Repositories
{
    public interface IThesisRepository
    {
        IEnumerable<Thesis> GetAll();
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

        public IEnumerable<Thesis> GetAll()
        {
            throw new NotImplementedException();
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
