using Microsoft.EntityFrameworkCore;
using ThesisManagement.Models;

namespace ThesisManagement.Repositories
{
    public interface ITopicRepository
    {
        bool Add(Topic topic);
        bool Update(Topic topic);
        bool Delete(int id);
        Topic? Get(int id);
        List<Topic> GetAll();
        List<Topic> GetMyTopicAndProfessorTopics(string studentId);
        List<Topic> GetAll(string professorId);
        List<Topic> Get(string topicName, string professorId);
        List<Topic> Get(string category, string technology, string professorname);

        bool CanBeDeleted(int topicId);
    }

    public class TopicRepository : BaseRepository, ITopicRepository
    {
        public TopicRepository() { }

        public bool Add(Topic topic)
        {
            _context.Add(topic);
            return DbSave();
        }

        public bool Delete(int id)
        {
            var topic = _context.Topics.FirstOrDefault(t => t.Id == id);
            if (topic == null) return false;
            _context.Remove(topic);
            return DbSave();
        }
        public bool Update(Topic topic)
        {
            _context.ChangeTracker.Clear();
            _context.Update(topic);
            return DbSave();
        }

        public Topic? Get(int id)
        {
            var topic = _context.Topics.Include(topic => topic.Professor).FirstOrDefault(topic => topic.Id == id);
            return topic;
        }

        public List<Topic> Get(string category, string technology, string professorname)
        {
            IEnumerable<Topic> filteredTopicList = _context.Topics.Include(t => t.Professor).Where(t => String.IsNullOrEmpty(t.StudentId));

            if (category != null)
            {
                filteredTopicList = filteredTopicList.Where(topic => topic.Category == category);
            }
            if (technology != null)
            {
                filteredTopicList = filteredTopicList.Where(topic => topic.Technology == technology);
            }
            if (professorname != null)
            {
                filteredTopicList = filteredTopicList.Where(topic => topic.Professor != null && topic.Professor.Name == professorname);
            }
            return filteredTopicList.ToList();
        }


        public List<Topic> GetAll(string professorId)
        {
            var topics = _context.Topics.Include(t => t.Professor)
                                        .Include(t => t.Theses)
                                        .Where(t => t.ProfessorId == professorId)
                                        .AsNoTracking().ToList();
            return topics;
        }

        public List<Topic> Get(string name, string professorId)
        {
            var topics = _context.Topics.Include(t => t.Professor)
                                        .Where(t => t.ProfessorId == professorId && t.Name.ToLower().Contains(name.ToLower()))
                                        .AsNoTracking().ToList();
            return topics;
        }

        public List<Topic> GetAll()
        {
            var topics = _context.Topics.Include(t => t.Professor)
                                       .Include(t => t.Theses)
                                       .AsNoTracking().ToList();
            return topics;
        }

        public List<Topic> GetMyTopicAndProfessorTopics(string studentId)
        {
            var topics = _context.Topics.Include(t => t.Professor)
                                        .Where(t => String.IsNullOrEmpty(t.StudentId))
                                       .AsNoTracking().ToList();

            var currentStudent = _context.Students.Include(s => s.Thesis)
                                            .ThenInclude(th => th.Topic)
                                            .ThenInclude(t => t.Professor)
                                            .FirstOrDefault(s => s.Id == studentId);
            if (currentStudent == null || currentStudent.ThesisId == null)
                return topics;

            var myTopic = currentStudent.Thesis!.Topic;

            if (myTopic.StudentId != null)
                topics.Insert(0, myTopic);
            else
                topics = topics.OrderByDescending(t => t.Id == myTopic.Id).ToList();

            topics[0].RegisteredStatusByCurrentUser = currentStudent.Thesis.TopicStatus;
            return topics;
        }

        public bool CanBeDeleted(int topicId)
        {
            bool hasStudents = _context.Theses.FirstOrDefault(t => t.TopicId == topicId) != null;
            return !hasStudents;
        }
    }
}