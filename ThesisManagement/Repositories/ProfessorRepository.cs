using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;
using System.Xml.Linq;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface IProfessorRepository
    {
        ObservableCollection<Professor> GetAll();
        ObservableCollection<string> GetProfessorNames();
        public IEnumerable<Topic> GetFilteredTopics(string category, string technology, string professorName);
    }

    public class ProfessorRepository : IProfessorRepository
    {
        private AppDbContext _context;
        public ProfessorRepository()
        {
            _context = DataProvider.Instance.Context;
        }

        public ObservableCollection<string> GetProfessorNames()
        {
            var professorNames = _context.Professors.Select(p => p.Name).ToList();
            return new ObservableCollection<string>(professorNames);
        }

        public ObservableCollection<Professor> GetAll()
        {
            var professors = _context.Professors.Include(t => t.Topics).AsNoTracking().ToList();
            return new ObservableCollection<Professor>(professors);
        }

        public IEnumerable<Topic> GetFilteredTopics(string category, string technology, string professorName)
        {
            IEnumerable<Topic> filteredTopicList = _context.Topics.Include(topic => topic.Professor);

            if (!string.IsNullOrEmpty(category))
            {
                filteredTopicList = filteredTopicList.Where(topic => topic.Category == category);
            }

            if (!string.IsNullOrEmpty(technology))
            {
                filteredTopicList = filteredTopicList.Where(topic => topic.Technology == technology);
            }

            if (!string.IsNullOrEmpty(professorName))
            {
                filteredTopicList = filteredTopicList.Where(topic => topic.Professor.Name == professorName);
            }

            return filteredTopicList.ToList();
        }

    }
}
