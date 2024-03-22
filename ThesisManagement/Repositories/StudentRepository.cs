using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface IStudentRepository
    {
        bool Update(Student student);
        List<Student> GetAll();
        List<Student> Get(string? filter);
        Student GetStudent(string id);
        Thesis? GetThesis(string studentId);
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

        public List<Student> GetAll()
        {
            var students = _context.Students.AsNoTracking().ToList();
            return students;
        }

        public List<Student> Get(string? filter)
        {
            if (String.IsNullOrEmpty(filter))
                return GetAll();

            var students = _context.Students.Where(s => s.Name.ToLower().Contains(filter.ToLower()) || s.Id.ToLower().Contains(filter.ToLower())).AsNoTracking().ToList();
            return students;
        }

        public Student GetStudent(string id)
        {
            var student = _context.Students.Where(st => st.Id == id).FirstOrDefault();
            return student;
        }

        public Thesis? GetThesis(string studentId)
        {
            var student = _context.Students.Include(s => s.Thesis)
                                            .ThenInclude(t => t.Topic)
                                            .FirstOrDefault(s => s.Id == studentId);
            return student?.Thesis;
        }
    }
}
