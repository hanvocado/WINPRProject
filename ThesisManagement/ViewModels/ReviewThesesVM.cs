using System.Windows;
using System.Windows.Input;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Professor;
using Task = System.Threading.Tasks.Task;

namespace ThesisManagement.ViewModels
{
    public class ReviewThesesVM : ViewModelBase
    {
        private readonly string currentUserId;
        private readonly IThesisRepository _thesisRepo;
        private IEnumerable<Thesis> waitingTheses;
        private IEnumerable<Thesis> approvedTheses;
        private Thesis selectedThesis;

        public Thesis SelectedThesis { get => selectedThesis; set { selectedThesis = value; OnPropertyChanged(nameof(SelectedThesis)); } }
        public IEnumerable<Thesis> WaitingTheses { get => waitingTheses; set { waitingTheses = value; OnPropertyChanged(nameof(WaitingTheses)); } }
        public IEnumerable<Thesis> ApprovedTheses { get => approvedTheses; set { approvedTheses = value; OnPropertyChanged(nameof(ApprovedTheses)); } }

        private Visibility visibleRestoreButton = Visibility.Hidden;
        public Visibility VisibleRestoreButton { get => visibleRestoreButton; set { visibleRestoreButton = value; OnPropertyChanged(); } }
        private int timer = 0;
        public int Timer { get => timer; set { timer = value; OnPropertyChanged(); LoadMessageRemain(); } }
        public string messageTimeRemain = "";
        public string MessageTimeRemain { get => messageTimeRemain; set { messageTimeRemain = value; OnPropertyChanged(); } }

        public ICommand ApproveCommand { get; set; }
        public ICommand RejectCommand { get; set; }
        public ICommand UndoCommand { get; set; }
        public ICommand ShowThesisCommand { get; set; }

        public ReviewThesesVM()
        {
            _thesisRepo = new ThesisRepository();
            currentUserId = SessionInfo.UserId;
            selectedThesis = new Thesis();
            WaitingTheses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Waiting);
            ApprovedTheses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Approved);
            ApproveCommand = new ViewModelCommand(ExecuteApproveCommand);
            RejectCommand = new ViewModelCommand(ExecuteRejectCommand);
            UndoCommand = new ViewModelCommand(ExecuteUndoCommand);
            ShowThesisCommand = new ViewModelCommand(ExecuteShowThesisCommand);
        }

        private void ExecuteApproveCommand(object obj)
        {
            bool canRegister = _thesisRepo.CanRegisterTopic(selectedThesis.Id);
            if (canRegister)
            {
                selectedThesis.TopicStatus = Variable.StatusTopic.Approved;
                _thesisRepo.Update(selectedThesis);
                WaitingTheses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Waiting);
            }
            ApprovedTheses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Approved);
        }

        private void ExecuteRejectCommand(object obj)
        {
            selectedThesis.TopicStatus = Variable.StatusTopic.Rejected;
            _thesisRepo.Update(selectedThesis);
            WaitingTheses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Waiting);
        }

        private void ExecuteUndoCommand(object obj)
        {
            ShowRestoreButton();
            StartTimer();
        }

        private void ShowRestoreButton()
        {
            VisibleRestoreButton = Visibility.Visible;
            Timer = 10;
        }

        private void HideRestoreButton()
        {
            VisibleRestoreButton = Visibility.Hidden;
            Timer = 0;
        }

        private async void StartTimer()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            Timer--;
            if (Timer > 0)
                StartTimer();
            else
                HideRestoreButton();
        }

        private void LoadMessageRemain()
        {
            MessageTimeRemain = "";
            if (timer > 0)
                MessageTimeRemain = "Còn " + timer + " giây để hoàn tác!";
        }

        private void ExecuteShowThesisCommand(object obj)
        {
            var vm = new MyThesisVM
            {
                Thesis = selectedThesis
            };
            var thesisView = new ThesisView { DataContext = vm };
            thesisView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            thesisView.Show();
        }
    }
}
