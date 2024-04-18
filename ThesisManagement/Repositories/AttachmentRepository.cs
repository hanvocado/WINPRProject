using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Attachment = ThesisManagement.Models.Attachment;

namespace ThesisManagement.Repositories
{
    public interface IAttachmentRepository
    {
        bool Add(Attachment attachment);
        bool AddRange(IEnumerable<Attachment>? attachments);
        bool Update(Attachment attachment);
        bool Delete(int id);
        ObservableCollection<Attachment> GetAll();
        ObservableCollection<Attachment> GetAttachments(int taskProgressId);
    }

    public class AttachmentRepository : BaseRepository, IAttachmentRepository
    {
        public AttachmentRepository() { }

        public bool Add(Attachment attachment)
        {
            _context.Add(attachment);
            return DbSave();
        }

        public bool Delete(int id)
        {
            var attachment = _context.Attachments.FirstOrDefault(at => at.Id == id);
            if (attachment == null) return false;
            _context.Remove(attachment);
            return DbSave();
        }
        public bool Update(Attachment attachment)
        {
            _context.ChangeTracker.Clear();
            _context.Update(attachment);
            return DbSave();
        }

        public ObservableCollection<Attachment> GetAll()
        {
            var attachments = _context.Attachments.Include(tp => tp.TaskProgress).AsNoTracking().ToList();
            return new ObservableCollection<Attachment>(attachments);
        }

        public ObservableCollection<Attachment> GetAttachments(int taskProgressId)
        {
            var attachments = _context.Attachments.Include(tp => tp.TaskProgress)
                                                  .Where(at => at.TaskProgressId == taskProgressId)
                                                  .AsNoTracking()
                                                  .ToList() ?? new List<Attachment>();
            return new ObservableCollection<Attachment>(attachments);
        }

        public bool AddRange(IEnumerable<Attachment>? attachments)
        {
            if (attachments?.Count() > 0)
            {
                _context.Attachments.AddRange(attachments);
                return DbSave();
            }
            return false;
        }
    }
}
