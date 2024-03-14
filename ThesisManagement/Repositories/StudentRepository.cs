using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface IStudentRepository
    {
        ObservableCollection<Student> GetAll();
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
    }
}
