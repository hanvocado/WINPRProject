using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisManagement.Models
{
    public class Student : User
    {
        public int? ThesisId { get; set; }

        [ForeignKey(nameof(ThesisId))]
        public Thesis? Thesis { get; set; }
    }
}
