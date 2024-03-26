﻿using System.ComponentModel.DataAnnotations;
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
    }
}
