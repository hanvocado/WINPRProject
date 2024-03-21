using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using ThesisManagement.Helpers;
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
        public ObservableCollection<Topic> GetFilteredTopics(string name, string category, string technology);

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

        public ObservableCollection<Topic> GetFilteredTopics(string name, string category, string technology)
        {
            IEnumerable<Topic> filteredTopicList = _context.Topics;

            if (name != null)
            {
                filteredTopicList = filteredTopicList.Where(topic => topic.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) != -1);
            }
            if (category != null)
            {
                filteredTopicList = filteredTopicList.Where(topic => topic.Category == category);
            }
            if (technology != null)
            {
                filteredTopicList = filteredTopicList.Where(topic => topic.Technology == technology);
            }
            return new ObservableCollection<Topic>(filteredTopicList);
        }


        public ObservableCollection<Topic> GetAll()
        {
            var topics = _context.Topics.Include(t => t.Professor)
                                        .Include(t => t.Theses)
                                        .AsNoTracking().ToList();
            return new ObservableCollection<Topic>(topics);
        }

    }
}