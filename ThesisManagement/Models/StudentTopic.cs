// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
// using ThesisManagement.Helpers;

// namespace ThesisManagement.Models
// {
//     public class StudentTopic
//     {
//         [Required]
//         [StringLength(20)]
//         public string StudentId { get; set; }
//         [Required]
//         public int TopicId { get; set; }

//         [Required]
//         [StringLength(50)]
//         public string Status { get; set; }
//         public Student Student { get; set; }
//         public Topic Topic { get; set; }


//         [NotMapped]
//         public string StatusColor
//         {
//             get
//             {
//                 if (Status == Variable.StudentTopic.Waiting)
//                     return "Yellow";
//                 else if (Status == Variable.StudentTopic.Approved)
//                     return "Green";
//                 else
//                     return "Red";
//             }
//         }

//     }
// }
