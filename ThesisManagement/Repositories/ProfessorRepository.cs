using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface IProfessorRepository
    {
        ObservableCollection<Professor> GetAll();
        IEnumerable<string> GetNames();
        Professor? Get(string id);
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

        public IEnumerable<string> GetNames()
        {
            var names = _context.Professors.Select(prof => prof.Name).ToList();
            return names;
        }
    }
}
