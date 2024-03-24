using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface ITopicRepository
    {
        bool Add(Topic topic);
        bool Update(Topic topic);
        bool Delete(int id);
        Topic? Get(int id);
        ObservableCollection<Topic> GetAll();
        ObservableCollection<Topic> GetMyTopicsAndProfessorTopics(string studentId);
        ObservableCollection<Topic> GetAll(string professorId);
        ObservableCollection<Topic> GetByTopicName(string name);
        ObservableCollection<Topic> GetFilteredTopics(string category, string technology, string professorname);

        bool CanBeDeleted(int topicId);
    }

    public class TopicRepository : ITopicRepository
    {
        private AppDbContext _context;
        private Topic? topic;
        public TopicRepository()
        {
            _context = DataProvider.Instance.Context;
        }
        public bool Add(Topic topic)
        {
            _context.Add(topic);
            return DbSave();
        }

        public bool Delete(int id)
        {
            topic = _context.Topics.FirstOrDefault(t => t.Id == id);
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

        public Topic? Get(int id)
        {
            topic = _context.Topics.Include(topic => topic.Professor).FirstOrDefault(topic => topic.Id == id);
            return topic;
        }

        public ObservableCollection<Topic> GetFilteredTopics(string category, string technology, string professorname)
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
            return new ObservableCollection<Topic>(filteredTopicList);
        }


        public ObservableCollection<Topic> GetAll(string professorId)
        {
            var topics = _context.Topics.Include(t => t.Professor)
                                        .Include(t => t.Theses)
                                        .Where(t => t.ProfessorId == professorId)
                                        .AsNoTracking().ToList();
            return new ObservableCollection<Topic>(topics);
        }

        public ObservableCollection<Topic> GetByTopicName(string name)
        {
            var topics = _context.Topics.Include(t => t.Professor)
                                        .Where(t => t.Name.ToLower().Contains(name.ToLower()))
                                        .AsNoTracking().ToList();
            return new ObservableCollection<Topic>(topics);
        }

        public ObservableCollection<Topic> GetAll()
        {
            var topics = _context.Topics.Include(t => t.Professor)
                                       .Include(t => t.Theses)
                                       .AsNoTracking().ToList();
            return new ObservableCollection<Topic>(topics);
        }

        public ObservableCollection<Topic> GetMyTopicsAndProfessorTopics(string studentId)
        {
            var topics = _context.Topics.Include(t => t.Professor)
                                       .Include(t => t.Theses)
                                       .Where(t => String.IsNullOrEmpty(t.StudentId) || t.StudentId == studentId)
                                       .AsNoTracking().OrderByDescending(t => !String.IsNullOrEmpty(t.StudentId));
            return new ObservableCollection<Topic>(topics);
        }

        public bool CanBeDeleted(int topicId)
        {
            bool hasStudents = _context.Theses.FirstOrDefault(t => t.TopicId == topicId) != null;
            return !hasStudents;
        }
    }
}