using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ThesisManagement.Models;
using ThesisManagement.Repositories.EF;

namespace ThesisManagement.Repositories
{
    public interface IScheduleRepository
    {
        bool Add(ScheduleInfo scheduleInfo);
        bool Update(ScheduleInfo scheduleInfo);
        bool Delete(int id);
        IEnumerable<ScheduleInfo> GetScheduleInfos(int thesisId);
    }

    public class ScheduleRepository : IScheduleRepository
    {
        private AppDbContext _context;

        public ScheduleRepository()
        {
            _context = DataProvider.Instance.Context;
        }

        public bool Add(ScheduleInfo scheduleInfo)
        {
            _context.Add(scheduleInfo);
            return DbSave();
        }

        public bool Update(ScheduleInfo scheduleInfo)
        {
            _context.Update(scheduleInfo);
            return DbSave();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
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

        public IEnumerable<ScheduleInfo> GetScheduleInfos(int thesisId)
        {
            var meetings = _context.ScheduleInfos.Include(th => th.Thesis)
                                                  .Where(sch => sch.ThesisId == thesisId)
                                                  .AsNoTracking()
                                                  .ToList();
            return meetings;
        }
    }
}
