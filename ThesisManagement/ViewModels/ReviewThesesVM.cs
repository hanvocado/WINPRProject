using System.Windows;
using System.Windows.Input;
using ThesisManagement.CustomControls;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;

namespace ThesisManagement.ViewModels
{
    public class ReviewThesesVM : ViewModelBase
    {
        private readonly string currentUserId;
        private readonly IThesisRepository _thesisRepo;
        private readonly DialogService _dialogService;

        private Thesis selectedThesis;
        public Thesis SelectedThesis { get => selectedThesis; set { selectedThesis = value; OnPropertyChanged(nameof(SelectedThesis)); } }

        private IEnumerable<Thesis> waitingTheses;
        public IEnumerable<Thesis> WaitingTheses { get => waitingTheses; set { waitingTheses = value; OnPropertyChanged(nameof(WaitingTheses)); } }

        private Visibility visibleUndoButton = Visibility.Hidden;
        public Visibility VisibleUndoButton
        {
            get { return visibleUndoButton; }
            set
            {
                visibleUndoButton = value;
                OnPropertyChanged(nameof(VisibleUndoButton));
            }
        }

        private int timer = 0;
        public int Timer
        {
            get => timer;
            set
            {
                timer = value;
                OnPropertyChanged();
                LoadTimeRemain();
            }
        }

        public string messageTimeRemain = "";
        public string MessageTimeRemain { get => messageTimeRemain; set { messageTimeRemain = value; OnPropertyChanged(); } }

        public ICommand ApproveCommand { get; set; }
        public ICommand UndoCommand { get; set; }
        public ICommand RejectCommand { get; set; }
        public ICommand ShowThesisCommand { get; set; }

        public ReviewThesesVM()
        {
            _thesisRepo = new ThesisRepository();
            _dialogService = new DialogService();
            currentUserId = SessionInfo.UserId;
            selectedThesis = new Thesis();
            WaitingTheses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Waiting);
            ApproveCommand = new ViewModelCommand(ExecuteApproveCommand);
            UndoCommand = new ViewModelCommand(ExecuteUndoCommand);
            RejectCommand = new ViewModelCommand(ExecuteRejectCommand);
        }

        private void ExecuteApproveCommand(object obj)
        {
            bool? confirmApprove = _dialogService.ShowDialog(Message.Notification, Message.ApproveNotification);
            if (confirmApprove == true)
            {
                LoadUndoButton();
                selectedThesis.TopicStatus = Variable.StatusTopic.Approved;
                var success = _thesisRepo.Update(selectedThesis);
                ShowMessage(success, Message.ApproveSuccess, Message.ApproveFailed);
            }
        }

        private void ExecuteRejectCommand(object obj)
        {
            bool? confirmReject = _dialogService.ShowDialog(Message.Notification, Message.RejectNotification);
            if (confirmReject == true)
            {
                LoadUndoButton();
                selectedThesis.TopicStatus = Variable.StatusTopic.Rejected;
                var success = _thesisRepo.Update(selectedThesis);
                ShowMessage(success, Message.RejectSuccess, Message.RejectFailed);
            }
        }

        private void ExecuteUndoCommand(object obj)
        {
            bool? confirmUndo = _dialogService.ShowDialog(Message.Notification, Message.UndoApproveNotification);
            if (confirmUndo == true)
            {
                HideUndoButton();
                selectedThesis.TopicStatus = Variable.StatusTopic.Waiting;
                var success = _thesisRepo.Update(selectedThesis);
                ShowMessage(success, Message.UndoApproveSuccess, Message.UndoApproveFailed);
            }
            else
            {
                WaitingTheses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Waiting);
                Window profWindow = Application.Current.MainWindow;
                profWindow.DataContext = new ProfessorMainVM { CurrentChildView = new ThesesVM() };
            }
        }

        private void LoadUndoButton()
        {
            ShowUndoButton();
            StartTimer();
        }

        private void HideUndoButton()
        {
            VisibleUndoButton = Visibility.Hidden;
            Timer = 0;
        }

        private void ShowUndoButton()
        {
            VisibleUndoButton = Visibility.Visible;
            Timer = 15;
        }

        private async void StartTimer()
        {
            await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1));
            Timer--;
            if (Timer > 0)
                StartTimer();
            else
                HideUndoButton();
        }

        private void LoadTimeRemain()
        {
            MessageTimeRemain = "";
            if (timer > 0)
                MessageTimeRemain = "Còn " + timer + " giây để hoàn tác!";
        }

    }
}
