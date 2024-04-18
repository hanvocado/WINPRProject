using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public abstract class BaseRepository
    {
        protected AppDbContext _context;

        protected BaseRepository()
        {
            _context = DataProvider.Instance.Context;
        }
        protected bool DbSave()
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
    }
}
