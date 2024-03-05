namespace ThesisManagement.ViewModels
{
    public class TopicVM
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Technology { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Categories { get; set; } = new List<string>() { "Education", "Health", "Business", "Other" };
        public IEnumerable<string> Technologies { get; set; } = new List<string>() { "JavaScript", "Wpf", ".NET", "Java", "Python", "Other" };
    }
}
