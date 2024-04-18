using Microsoft.EntityFrameworkCore;
using ThesisManagement.Models;

namespace ThesisManagement.Repositories
{
    public interface IScheduleRepository
    {
        bool Add(ScheduleInfo scheduleInfo);
        bool Update(ScheduleInfo scheduleInfo);
        bool Delete(int id);
        IEnumerable<ScheduleInfo> GetScheduleInfos(int thesisId);
        int CountUpcomingSchedules(int thesisId);
    }

    public class ScheduleRepository : BaseRepository, IScheduleRepository
    {
        public ScheduleRepository() { }

        public bool Add(ScheduleInfo scheduleInfo)
        {
            _context.Add(scheduleInfo);
            return DbSave();
        }

        public bool Update(ScheduleInfo scheduleInfo)
        {
            _context.ChangeTracker.Clear();
            _context.Update(scheduleInfo);
            return DbSave();
        }

        public bool Delete(int id)
        {
            var schedule = _context.ScheduleInfos.FirstOrDefault(s => s.Id == id);
            if (schedule == null) return false;
            _context.Remove(schedule);
            return DbSave();
        }

        public IEnumerable<ScheduleInfo> GetScheduleInfos(int thesisId)
        {
            var meetings = _context.ScheduleInfos.Include(th => th.Thesis)
                                                  .Where(sch => sch.ThesisId == thesisId)
                                                  .AsNoTracking()
                                                  .ToList();
            return meetings;
        }

        public int CountUpcomingSchedules(int thesisId)
        {
            var count = _context.ScheduleInfos.Include(th => th.Thesis)
                                                  .Where(sch => sch.ThesisId == thesisId && sch.From > DateTime.Now)
                                                  .AsNoTracking()
                                                  .Count();
            return count;
        }
    }
}
