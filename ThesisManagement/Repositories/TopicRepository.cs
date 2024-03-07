using Microsoft.EntityFrameworkCore;
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
        IEnumerable<Topic> GetAll();
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

        public void Update(Topic topic)
        {

        }

        public void Delete(int id)
        {
            topic = _context.Topics.FirstOrDefault(t => t.Id == id);
            if (topic == null) return;
            _context.Remove(topic);
            DbSave();
        }

        public Topic? Get(int id)
        {
            topic = _context.Topics.Include(topic => topic.Professor).FirstOrDefault(topic => topic.Id == id);
            return topic;
        }

        public IEnumerable<Topic> GetAll()
        {
            var topics = _context.Topics.Include(t => t.Professor).AsNoTracking().ToList();
            return topics;
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
            }
        }
    }
}
