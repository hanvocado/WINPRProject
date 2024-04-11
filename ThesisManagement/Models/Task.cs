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
        public string? UpdateCount
        {
            get
            {
                if (TaskProgresses == null || TaskProgresses.Count == 0)
                    return null;

                int count = 0;
                foreach (var progress in this.TaskProgresses)
                {
                    if (String.IsNullOrEmpty(progress.Response))
                        ++count;
                    if (count > 2)
                        return count.ToString() + "+";
                }

                if (count > 0)
                    return count.ToString();
                return null;
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
