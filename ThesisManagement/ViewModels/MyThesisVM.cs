using System.Windows.Input;
using ThesisManagement.Helpers;
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
            set
            {
                thesis = value;
                Topic = thesis.Topic;
                Thesis.Students = _studentRepo.GetStudent(Thesis.Id)?.ToList() ?? new List<Student>();
                OnPropertyChanged(nameof(Thesis));
            }
        }

        private string evaluation;
        public string Evaluation
        {
            get { return evaluation; }
            set { evaluation = value; OnPropertyChanged(nameof(Evaluation)); }
        }

        private int score;
        public int Score
        {
            get { return score; }
            set { score = value; OnPropertyChanged(nameof(Score)); }
        }

        public string DetailsVisibility
        {
            get
            {
                return this.Thesis.TopicStatus == Variable.StatusTopic.Approved ? "Visible" : "Collapsed";
            }
        }


        public ICommand MakeEvaluationCommand { get; set; }

        public MyThesisVM()
        {
            _thesisRepo = new ThesisRepository();
            _professorRepo = new ProfessorRepository();
            _topicRepo = new TopicRepository();
            _studentRepo = new StudentRepository();
            Thesis ??= _studentRepo.GetThesis(SessionInfo.UserId) ?? new Thesis();
            MakeEvaluationCommand = new ViewModelCommand(ExecuteMakeEvaluationCommand);
        }

        private void ExecuteMakeEvaluationCommand(object obj)
        {
            thesis.Evaluation = evaluation;
            thesis.Score = score;
            var success = _thesisRepo.Update(thesis);
            ShowMessage(success, Message.UpdateSuccess, Message.UpdateFailed);
        }
    }
}
