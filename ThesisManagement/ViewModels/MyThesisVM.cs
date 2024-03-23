using System.Diagnostics;
using System.Windows;
using ThesisManagement.Models;
using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class MyThesisVM : ViewModelBase
    {
        private readonly ITopicRepository _topicRepo;
        private readonly IThesisRepository _thesisRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IProfessorRepository _professorRepo;

        private Topic topic;

        public Topic Topic
        {
            get { return topic; }
            set { topic = value; OnPropertyChanged(nameof(Topic)); }
        }  

        private Thesis thesis;

        public Thesis Thesis
        {
            get { return thesis; }
            set { thesis = value; OnPropertyChanged(nameof(Thesis)); }
        }


        public MyThesisVM()
        {
            _thesisRepo = new ThesisRepository();
            _professorRepo = new ProfessorRepository();
            _topicRepo = new TopicRepository();
            _studentRepo = new StudentRepository();
            Thesis = _studentRepo.GetThesis(SessionInfo.UserId) ?? new Thesis();
            Topic = _topicRepo.Get(Thesis.TopicId) ?? new Topic();

            Trace.WriteLine("Thesis:");
            Trace.WriteLine($"  Id: {Thesis.Id}");

            Trace.WriteLine("Topic:");
            Trace.WriteLine($"  Id: {Topic.Id}");
            Trace.WriteLine($"  Name: {Topic.Name}");
            Trace.WriteLine($"  Thể loại: {Topic.Category}");
            Trace.WriteLine($"  Công nghệ: {Topic.Technology}");
            Trace.WriteLine($"  Chức năng: {Topic.Function}");

            Trace.WriteLine("Professor:");
            Trace.WriteLine($"  Id: {Topic.ProfessorId}");
            Trace.WriteLine($"  Name: {Topic.Professor.Name}");

            Trace.WriteLine("Thành viên:");
            foreach(var student in Thesis.Students)
            {
                Trace.WriteLine($"  Name: {student.Name}");
            }

        }
    }
}
