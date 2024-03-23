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

        private IEnumerable<Student> students;

        public IEnumerable<Student> Students
        {
            get { return students; }
            set { students = value; OnPropertyChanged(nameof(Students)); }
        }


        public MyThesisVM()
        {
            _thesisRepo = new ThesisRepository();
            _professorRepo = new ProfessorRepository();
            _topicRepo = new TopicRepository();
            _studentRepo = new StudentRepository();
            Thesis = _studentRepo.GetThesis(SessionInfo.UserId) ?? new Thesis();
            Topic = _topicRepo.Get(Thesis.TopicId) ?? new Topic();
            Students = _studentRepo.GetStudent(Thesis.Id)?.ToList() ?? new List<Student>();

        }
    }
}
