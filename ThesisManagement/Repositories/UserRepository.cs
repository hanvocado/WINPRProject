using System.Net;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface IUserRepository
    {
        bool Authenticate(NetworkCredential credential);
    }

    public class UserRepository : IUserRepository
    {
        private AppDbContext _context;
        private User? user;
        public UserRepository()
        {
            _context = DataProvider.Instance.Context;
        }
        public bool Authenticate(NetworkCredential credential)
        {
            var email = credential.UserName.ToLower();
            var pw = credential.Password;
            user = _context.Students.FirstOrDefault(p => p.Email.ToLower() == email && p.Password == pw);
            if (user != null)
            {
                SessionInfo.UserId = user.Id;
                SessionInfo.Name = user.Name;
                SessionInfo.Role = Role.Student;
                return true;
            }

            user = _context.Professors.FirstOrDefault(p => p.Email.ToLower() == email && p.Password == pw);
            if (user != null)
            {
                SessionInfo.UserId = user.Id;
                SessionInfo.Name = user.Name;
                SessionInfo.Role = Role.Professor;
                return true;
            }

            user = _context.Admins.FirstOrDefault(p => p.Email.ToLower() == email && p.Password == pw);
            if (user != null)
            {
                SessionInfo.UserId = user.Id;
                SessionInfo.Name = user.Name;
                SessionInfo.Role = Role.Admin;
                return true;
            }

            return false;
        }
    }
}
