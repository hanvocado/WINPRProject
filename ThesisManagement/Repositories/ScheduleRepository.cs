using Microsoft.EntityFrameworkCore;
using ThesisManagement.Models;

namespace ThesisManagement.Repositories
{
    public interface IScheduleRepository
    {
        bool Add(ScheduleInfo scheduleInfo);
        bool Update(ScheduleInfo scheduleInfo);
        bool Delete(int id);
        IEnumerable<ScheduleInfo> Get(int thesisId);
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

        public IEnumerable<ScheduleInfo> Get(int thesisId)
        {
            var meetings = _context.ScheduleInfos.Where(sch => sch.ThesisId == thesisId)
                                                  .AsNoTracking().ToList();
            return meetings;
        }
    }
}
