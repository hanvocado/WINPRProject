using Microsoft.EntityFrameworkCore;
using ThesisManagement.Helpers;
using ThesisManagement.Models;

namespace ThesisManagement.Repositories
{
    public interface IStudentRepository
    {
        bool Update(Student student);
        List<Student> GetAll();
        Student? GetStudent(string id);
        IEnumerable<Student> GetStudent(int thesisId);
        List<Student> GetUnRegisteredStudents();
        Thesis? GetThesis(string studentId);
        bool CanRegisterTopic(string studentId);
        float UpdateWorkTime(string studentId, float timeToAdd);
    }
    public class StudentRepository : BaseRepository, IStudentRepository
    {
        public StudentRepository() { }

        public bool Update(Student student)
        {
            _context.Update(student);
            return DbSave();
        }

        public List<Student> GetAll()
        {
            var students = _context.Students.AsNoTracking().ToList();
            return students;
        }

        public Student? GetStudent(string id)
        {
            var student = _context.Students.Where(st => st.Id == id).FirstOrDefault();
            return student;
        }

        public IEnumerable<Student> GetStudent(int thesisId)
        {
            var students = _context.Students.Include(th => th.Thesis).Where(st => st.ThesisId == thesisId).ToList();
            return students;
        }

        public Thesis? GetThesis(string studentId)
        {
            var student = _context.Students.Include(s => s.Thesis)
                                            .ThenInclude(t => t.Topic)
                                            .ThenInclude(t => t.Professor)
                                            .FirstOrDefault(s => s.Id == studentId);
            return student?.Thesis;
        }

        public bool CanRegisterTopic(string studentId)
        {
            var student = _context.Students.Include(s => s.Thesis).FirstOrDefault(s => s.Id == studentId);
            if (student?.ThesisId == null || student?.Thesis?.TopicStatus == Variable.StatusTopic.Rejected)
                return true;

            return false;
        }

        public List<Student> GetUnRegisteredStudents()
        {
            var unRegisterStd = _context.Students
                                        .Include(st => st.Thesis)
                                        .Where(st => st.ThesisId == null || st.Thesis.TopicStatus == Variable.StatusTopic.Rejected)
                                        .ToList();
            return unRegisterStd;
        }

        public float UpdateWorkTime(string studentId, float timeToAdd)
        {
            var student = _context.Students.FirstOrDefault(t => t.Id == studentId);
            if (student != null)
            {
                student.WorkingTime += timeToAdd;
                DbSave();
            }
            return student?.WorkingTime ?? 0;
        }
    }
}
