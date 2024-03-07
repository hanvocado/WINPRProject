namespace ThesisManagement.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string ProfessorId { get; set; }
        public string? StudentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Category { get; set; }
        public string? Technology { get; set; }
    }
}
