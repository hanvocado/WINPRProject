using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ThesisManagement.Repositories.EF;
using Attachment = ThesisManagement.Models.Attachment;

namespace ThesisManagement.Repositories
{
    public interface IAttachmentRepository
    {
        bool Add(Attachment attachment);
        bool Update(Attachment attachment);
        bool Delete(int id);
        ObservableCollection<Attachment> GetAll();
        IEnumerable<Attachment> GetAttachments(int taskProgressId);
    }

    public class AttachmentRepository : IAttachmentRepository
    {
        private AppDbContext _context;
        private Attachment? attachment;
        public AttachmentRepository()
        {
            _context = DataProvider.Instance.Context;
        }
        public bool Add(Attachment attachment)
        {
            _context.Add(attachment);
            return DbSave();
        }

        public bool Delete(int id)
        {
            attachment = _context.Attachments.FirstOrDefault(at => at.Id == id);
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

        public ObservableCollection<Attachment> GetAll()
        {
            var attachments = _context.Attachments.Include(tp => tp.TaskProgress).AsNoTracking().ToList();
            return new ObservableCollection<Attachment>(attachments);
        }

        public IEnumerable<Attachment> GetAttachments(int taskProgressId)
        {
            var attachments = _context.Attachments.Include(tp => tp.TaskProgress)
                                                             .Where(at => at.TaskProgressId == taskProgressId)
                                                             .AsNoTracking()
                                                             .ToList();
            return attachments;
        }
    }
}
