//using Microsoft.EntityFrameworkCore;
//using ThesisManagement.Helpers;
//using ThesisManagement.Models;
//using ThesisManagement.Repositories.EF;

//namespace ThesisManagement.Repositories
//{
//    public interface IStudentTopicRepository
//    {
//        void Add(StudentTopic topic);
//        void Update(StudentTopic topic);
//        void Delete(int id);
//        IEnumerable<StudentTopic> GetAll();
//        IEnumerable<StudentTopic> Get(string studentId, string status);
//        IEnumerable<Student> GetMembers(int topicId);

//        void AddMembers(int topicId, IEnumerable<Student> members);

//        bool CanRegisterTopic(string studentId);

//    }
//    public class StudentTopicRepository : IStudentTopicRepository
//    {
//        private AppDbContext _context;

//        public StudentTopicRepository()
//        {
//            _context = DataProvider.Instance.Context;
//        }

//        public bool CanRegisterTopic(string studentId)
//        {
//            int waiting = _context.StudentTopics.Where(st => st.StudentId == studentId && st.Status == Variable.StudentTopic.Waiting).Count();
//            if (waiting >= 1)
//                return false;
//            return true;
//        }

//        public void Add(StudentTopic topic)
//        {
//            throw new NotImplementedException();
//        }


//        public void Delete(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<StudentTopic> Get(string studentId, string status)
//        {
//            var list = _context.StudentTopics.Include(st => st.Student).Include(st => st.Topic)
//                                    .ThenInclude(t => t.Professor)
//                                    .Where(st => st.StudentId == studentId && st.Status == status).AsNoTracking().ToList();
//            return list;
//        }

//        public IEnumerable<StudentTopic> GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        public void Update(StudentTopic topic)
//        {
//            throw new NotImplementedException();
//        }

//        public void AddMembers(int topicId, IEnumerable<Student> members)
//        {
//            var studentTopics = new List<StudentTopic>();
//            foreach (Student member in members)
//            {
//                studentTopics.Add(new StudentTopic() { StudentId = member.Id, TopicId = topicId, Status = Variable.StudentTopic.Waiting });
//            }
//            _context.StudentTopics.AddRange(studentTopics);
//        }

//        public IEnumerable<Student> GetMembers(int topicId)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
