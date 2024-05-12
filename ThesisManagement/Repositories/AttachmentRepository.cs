using Attachment = ThesisManagement.Models.Attachment;

namespace ThesisManagement.Repositories
{
    public interface IAttachmentRepository
    {
        bool AddRange(IEnumerable<Attachment>? attachments);
    }

    public class AttachmentRepository : BaseRepository, IAttachmentRepository
    {
        public AttachmentRepository() { }

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
