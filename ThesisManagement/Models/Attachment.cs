using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TaskProgressId { get; set; }

        [Required]
        public string FileName { get; set; }
        public string Sender { get; set; }

        [ForeignKey(nameof(TaskProgressId))]
        public TaskProgress TaskProgress { get; set; }

        [NotMapped]
        public string OriginalFileName
        {
            get
            {
                return FileName.Substring(14);
            }
        }
    }
}
