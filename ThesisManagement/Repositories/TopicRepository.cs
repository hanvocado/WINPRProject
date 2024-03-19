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
        void Add(Topic topic);
        void Update(Topic topic);
        void Delete(int id);
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
        public void Add(Topic topic)
        {
            _context.Add(topic);
            DbSave();
        }

        public void Delete(int id)
        {
            try
            {
                topic = _context.Topics.FirstOrDefault(t => t.Id == id);
                if (topic == null) return;
                _context.Remove(topic);
                _context.SaveChanges();
            }
            catch
            {
                ShowErrorMessage(Message.DeleteFailed);
            }
        }
        public void Update(Topic topic)
        {
            _context.ChangeTracker.Clear();
            _context.Update(topic);
            DbSave();
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

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void DbSave()
        {
            try
            {
                _context.SaveChanges();
                ShowSuccessMessage(Message.Success);
            }
            catch (DbUpdateException e)
            {
                ShowErrorMessage(e.Message);
                Trace.WriteLine(e);
            }
        }
    }
}