using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ThesisManagement.Models;

namespace ThesisManagement.Repositories
{
    public interface IProfessorRepository
    {
        ObservableCollection<Professor> GetAll();
        Professor? Get(string id);
        bool HasNewUpdate(string professorId);
    }

    public class ProfessorRepository : BaseRepository, IProfessorRepository
    {
        public ProfessorRepository() { }

        public Professor? Get(string id)
        {
            var professor = _context.Professors.Include(pr => pr.Topics).FirstOrDefault(pr => pr.Id == id);
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
