using ThesisManagement.Models;
using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class StatusTopicsVM : ViewModelBase
    {
        private readonly IThesisRepository _repo;
        private IEnumerable<Thesis> waitingTopics;
        private IEnumerable<Thesis> approvedTopics;
        private IEnumerable<Thesis> rejectedTopics;
        public IEnumerable<Thesis> WaitingTopics
        {
            get { return waitingTopics; }
            set
            {
                waitingTopics = value;
                OnPropertyChanged(nameof(WaitingTopics));
            }
        }
        public IEnumerable<Thesis> ApprovedTopics
        {
            get { return approvedTopics; }
            set
            {
                approvedTopics = value;
                OnPropertyChanged(nameof(ApprovedTopics));
            }
        }
        public IEnumerable<Thesis> RejectedTopics
        {
            get { return rejectedTopics; }
            set
            {
                rejectedTopics = value;
                OnPropertyChanged(nameof(RejectedTopics));
            }
        }

        public StatusTopicsVM()
        {
            _repo = new ThesisRepository();
            WaitingTopics = _repo.Get(1, "Waiting");
            ApprovedTopics = _repo.Get(2, "Approved");
            RejectedTopics = _repo.Get(3, "Rejected");
        }
    }
}
