using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    public class Professor : User
    {
        public ICollection<Topic>? Topics { get; set; }
    }
}
