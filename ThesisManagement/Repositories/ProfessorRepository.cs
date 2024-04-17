using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface IProfessorRepository
    {
        ObservableCollection<Professor> GetAll();
        Professor? Get(string id);
        bool HasNewUpdate(string professorId);
    }

    public class ProfessorRepository : IProfessorRepository
    {
        private AppDbContext _context;
        private Professor? professor;

        public ProfessorRepository()
        {
            _context = DataProvider.Instance.Context;
        }

        public Professor? Get(string id)
        {
            professor = _context.Professors.Include(pr => pr.Topics).FirstOrDefault(pr => pr.Id == id);
            return professor;
        }

        public ObservableCollection<Professor> GetAll()
        {
            var professors = _context.Professors.Include(t => t.Topics).AsNoTracking().ToList();
            return new ObservableCollection<Professor>(professors);
        }

        public bool HasNewUpdate(string professorId)
        {
            var topics = _context.Professors.Include(p => p.Topics)
                                            .ThenInclude(t => t.Theses)
                                            .ThenInclude(th => th.Tasks)
                                            .FirstOrDefault(p => p.Id == professorId).Topics;
            if (topics != null)
                foreach (Topic topic in topics)
                {
                    if (topic?.Theses?.Any() ?? false)
                    {
                        foreach (Thesis thesis in topic.Theses)
                        {
                            if (thesis.HasNewUpdate)
                                return true;
                        }
                    }
                }

            return false;
        }
    }
}
