using System.Windows.Input;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class ReviewThesesVM : ViewModelBase
    {
        private readonly string currentUserId;
        private readonly IThesisRepository _thesisRepo;

        private Thesis selectedThesis;
        public Thesis SelectedThesis { get => selectedThesis; set { selectedThesis = value; OnPropertyChanged(nameof(SelectedThesis)); } }

        private IEnumerable<Thesis> waitingTheses;
        public IEnumerable<Thesis> WaitingTheses { get => waitingTheses; set { waitingTheses = value; OnPropertyChanged(nameof(WaitingTheses)); } }

        public ICommand ApproveCommand { get; set; }
        public ICommand RejectCommand { get; set; }
        public ICommand ShowThesisCommand { get; set; }

        public ReviewThesesVM()
        {
            _thesisRepo = new ThesisRepository();
            currentUserId = SessionInfo.UserId;
            selectedThesis = new Thesis();
            WaitingTheses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Waiting);
            ApproveCommand = new ViewModelCommand(ExecuteApproveCommand);
            RejectCommand = new ViewModelCommand(ExecuteRejectCommand);
        }

        private void ExecuteApproveCommand(object obj)
        {
            selectedThesis.TopicStatus = Variable.StatusTopic.Approved;
            var success = _thesisRepo.Update(selectedThesis);
            WaitingTheses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Waiting);
        }

        private void ExecuteRejectCommand(object obj)
        {
            selectedThesis.TopicStatus = Variable.StatusTopic.Rejected;
            _thesisRepo.Update(selectedThesis);
            WaitingTheses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Waiting);
        }
    }
}
