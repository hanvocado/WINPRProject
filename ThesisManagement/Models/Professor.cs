namespace ThesisManagement.Models
{
    public class Professor : User
    {
        public ICollection<Topic>? Topics { get; set; }
    }
}
