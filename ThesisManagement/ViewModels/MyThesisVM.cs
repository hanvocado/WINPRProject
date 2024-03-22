using System.Diagnostics;
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

        private Professor professor;

        public Professor Professor
        {
            get { return professor; }
            set { professor = value; OnPropertyChanged(nameof(Professor)); }
        }

        private Thesis thesis;

        public Thesis Thesis
        {
            get { return thesis; }
            set { thesis = value; OnPropertyChanged(nameof(Thesis)); }
        }

        private Student student;

        public Student Student
        {
            get { return student; }
            set { student = value; OnPropertyChanged(nameof(Student)); }
        }

        public MyThesisVM()
        {
            _thesisRepo = new ThesisRepository();
            _professorRepo = new ProfessorRepository();
            _topicRepo = new TopicRepository();
            _studentRepo = new StudentRepository();
            //Thesis = _studentRepo.GetThesis(SessionInfo.UserId) ?? new Thesis();
            //Topic = _topicRepo.Get(Thesis.TopicId) ?? new Topic();
            //Professor = _professorRepo.Get(Topic.ProfessorId) ?? new Professor();

            Trace.WriteLine("Thesis:");
            thesis = _studentRepo.GetThesis(SessionInfo.UserId) ?? new Thesis();
            Trace.WriteLine($"  Id: {thesis.Id}");
         

            Trace.WriteLine("Topic:");
            topic = _topicRepo.Get(thesis.TopicId) ?? new Topic();
            Trace.WriteLine($"  Id: {topic.Id}");
            Trace.WriteLine($"  Title: {topic.Name}");

            Trace.WriteLine("Professor:");
            professor = _professorRepo.Get(topic.ProfessorId) ?? new Professor();
            Trace.WriteLine($"  Id: {professor.Id}");
            Trace.WriteLine($"  Name: {professor.Name}");

        }
    }
}
