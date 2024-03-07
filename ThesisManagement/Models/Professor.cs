using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisManagement.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class Professor
    {
        [Key]
        [StringLength(20)]
        public string Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [StringLength(20)]
        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        [StringLength(12)]
        public string? Phone { get; set; }

        public DateTime? Birthday { get; set; }
        public ICollection<Topic>? Topics { get; set; }
    }
}
