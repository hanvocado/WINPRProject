﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    public class Student : User
    {
        public int? ThesisId { get; set; }

        [ForeignKey(nameof(ThesisId))]
        public Thesis? Thesis { get; set; }

        public float? Score { get; set; }
        
        public float WorkingTime { get; set; }

        public ICollection<TaskProgress>? TaskProgresses { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; } = false;
    }
}
