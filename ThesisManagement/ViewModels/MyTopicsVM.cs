using ThesisManagement.Models;
using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class MyTopicsVM : ViewModelBase
    {
        private readonly IStudentTopicRepository _repo;
        private IEnumerable<StudentTopic> waitingTopics;
        private IEnumerable<StudentTopic> approvedTopics;
        private IEnumerable<StudentTopic> rejectedTopics;
        public IEnumerable<StudentTopic> WaitingTopics
        {
            get { return waitingTopics; }
            set
            {
                waitingTopics = value;
                OnPropertyChanged(nameof(WaitingTopics));
            }
        }
        public IEnumerable<StudentTopic> ApprovedTopics
        {
            get { return approvedTopics; }
            set
            {
                approvedTopics = value;
                OnPropertyChanged(nameof(ApprovedTopics));
            }
        }
        public IEnumerable<StudentTopic> RejectedTopics
        {
            get { return rejectedTopics; }
            set
            {
                rejectedTopics = value;
                OnPropertyChanged(nameof(RejectedTopics));
            }
        }

        public MyTopicsVM()
        {
            _repo = new StudentTopicRepository();
            WaitingTopics = _repo.Get("S1", "Waiting");
            ApprovedTopics = _repo.Get("S1", "Approved");
            RejectedTopics = _repo.Get("S1", "Rejected");
        }
    }
}
