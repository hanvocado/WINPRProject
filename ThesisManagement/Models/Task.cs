using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ThesisId { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int Progress { get; set; }

        [ForeignKey(nameof(ThesisId))]
        public Thesis Thesis { get; set; }

        public ICollection<TaskProgress>? TaskProgresses { get; set; }

        [NotMapped]
        public int UpdateCount
        {
            get
            {
                if (TaskProgresses == null || TaskProgresses.Count == 0)
                    return 0;

                int res = 0;
                foreach (var progress in this.TaskProgresses)
                {
                    if (String.IsNullOrEmpty(progress.Response))
                        ++res;
                }
                return res;
            }
        }
    }

    public class TasksPie
    {
        public string TaskStatus { get; set; }
        public int Count { get; set; }
        public int Percentage { get; set; }

    }
}
