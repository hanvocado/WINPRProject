using System.Net;
using ThesisManagement.Models;

namespace ThesisManagement.Repositories
{
    public interface IUserRepository
    {
        bool Authenticate(NetworkCredential credential);
    }

    public class UserRepository : BaseRepository, IUserRepository
    {
        private User? user;
        public UserRepository() { }
        public bool Authenticate(NetworkCredential credential)
        {
            bool logined = false;
            var email = credential.UserName.ToLower();
            var pw = credential.Password;
            user = _context.Students.FirstOrDefault(p => p.Email.ToLower() == email);
            logined = LoginUser(Role.Student, pw);
            if (logined)
            {
                return true;
            }

            user = _context.Professors.FirstOrDefault(p => p.Email.ToLower() == email);
            return LoginUser(Role.Professor, pw);
        }

        private bool LoginUser(Role role, string pw)
        {
            if (user != null && user.Password == pw)
            {
                SessionInfo.UserId = user.Id;
                SessionInfo.Name = user.Name;
                SessionInfo.Role = role;
                return true;
            }
            return false;
        }
    }
}
