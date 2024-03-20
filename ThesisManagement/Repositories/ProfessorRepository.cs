using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface IProfessorRepository
    {
        ObservableCollection<Professor> GetAll();
    }

    public class ProfessorRepository : IProfessorRepository
    {
        private AppDbContext _context;
        public ProfessorRepository()
        {
            _context = DataProvider.Instance.Context;
        }

        public ObservableCollection<Professor> GetAll()
        {
            var professors = _context.Professors.Include(t => t.Topics).AsNoTracking().ToList();
            return new ObservableCollection<Professor>(professors);
        }

    }
}
