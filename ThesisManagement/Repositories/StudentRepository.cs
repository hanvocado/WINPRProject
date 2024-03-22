using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;
using static ThesisManagement.Helpers.Query;

namespace ThesisManagement.Repositories
{
    public interface IStudentRepository
    {
        bool Update(Student student);
        ObservableCollection<Student> GetAll();
        ObservableCollection<Student> Get(string filter);
        Student GetStudent(string id);
    }
    public class StudentRepository : IStudentRepository
    {
        private AppDbContext _context;
        public StudentRepository()
        {
            _context = DataProvider.Instance.Context;
        }

        public bool Update(Student student)
        {
            _context.ChangeTracker.Clear();
            _context.Update(student);
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

        public Student GetStudent(string id)
        {
            var student = _context.Students.Where(st => st.Id == id).FirstOrDefault();
            return student;
        }
    }
}
