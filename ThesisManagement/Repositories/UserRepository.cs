using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface IUserRepository
    {
        bool Authenticate(string username, string password);
    }

    public class UserRepository : IUserRepository
    {
        private AppDbContext _context;
        private User? user;
        public UserRepository()
        {
            _context = DataProvider.Instance.Context;
        }
        public bool Authenticate(string email, string password)
        {
            email = email.ToLower();
            user = _context.Students.FirstOrDefault(p => p.Email.ToLower() == email && p.Password == password);
            if (user != null)
            {
                SessionInfo.UserId = user.Id;
                SessionInfo.Role = Role.Student;
                return true;
            }

            user = _context.Professors.FirstOrDefault(p => p.Email.ToLower() == email && p.Password == password);
            if (user != null)
            {
                SessionInfo.UserId = user.Id;
                SessionInfo.Role = Role.Professor;
                return true;
            }

            user = _context.Admins.FirstOrDefault(p => p.Email.ToLower() == email && p.Password == password);
            if (user != null)
            {
                SessionInfo.UserId = user.Id;
                SessionInfo.Role = Role.Admin;
                return true;
            }

            return false;
        }
    }
}
