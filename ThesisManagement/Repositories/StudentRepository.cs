using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface IStudentRepository
    {
        ObservableCollection<Student> GetAll();
        ObservableCollection<Student> Get(string filter);
    }
    public class StudentRepository : IStudentRepository
    {
        private AppDbContext _context;
        public StudentRepository()
        {
            _context = DataProvider.Instance.Context;
        }

        public ObservableCollection<Student> GetAll()
        {
            var students = _context.Students.AsNoTracking().ToList();
            return new ObservableCollection<Student>(students);
        }
        public ObservableCollection<Student> Get(string filter)
        {
            var students = _context.Students.Where(s => s.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) || s.Id.Contains(filter, StringComparison.OrdinalIgnoreCase)).AsNoTracking().ToList();
            return new ObservableCollection<Student>(students);
        }
    }
}
