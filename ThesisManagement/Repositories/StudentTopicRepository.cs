using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface IStudentTopicRepository
    {
        void Add(StudentTopic topic);
        void Update(StudentTopic topic);
        void Delete(int id);
        ObservableCollection<StudentTopic> GetAll();
        ObservableCollection<StudentTopic> Get(string studentId);
        public IEnumerable<StudentTopic> Get(string studentId, string status);

    }
    public class StudentTopicRepository : IStudentTopicRepository
    {
        private AppDbContext _context;

        public StudentTopicRepository()
        {
            _context = DataProvider.Instance.Context;
        }

        public void Add(StudentTopic topic)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<StudentTopic> Get(string studentId)
        {
            var list = _context.StudentTopics.Include(st => st.Student).Include(st => st.Topic).Where(st => st.StudentId == studentId).ToList();
            return new ObservableCollection<StudentTopic>(list);
        }

        public IEnumerable<StudentTopic> Get(string studentId, string status)
        {
            var list = _context.StudentTopics.Include(st => st.Student).Include(st => st.Topic).
                                    ThenInclude(t => t.Professor)
                                    .Where(st => st.StudentId == studentId && st.Status == status).ToList();
            return new ObservableCollection<StudentTopic>(list);
        }

        public ObservableCollection<StudentTopic> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(StudentTopic topic)
        {
            throw new NotImplementedException();
        }
    }
}
