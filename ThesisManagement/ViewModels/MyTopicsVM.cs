using ThesisManagement.Models;
using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class MyTopicsVM
    {
        private readonly IStudentTopicRepository _repo;
        public IEnumerable<StudentTopic> WaitingTopics;
        public IEnumerable<StudentTopic> ApprovedTopics;
        public IEnumerable<StudentTopic> RejectedTopics;

        public MyTopicsVM()
        {
            _repo = new StudentTopicRepository();
            WaitingTopics = _repo.Get("S1", "Waiting");
            ApprovedTopics = _repo.Get("S1", "Approved");
            RejectedTopics = _repo.Get("S1", "Rejected");
        }
    }
}
