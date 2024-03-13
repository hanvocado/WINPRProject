using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using Task = System.Threading.Tasks.Task;

namespace ThesisManagement.ViewModels
{
    public class ReviewThesesVM : ViewModelBase
    {
        private readonly IThesisRepository _thesisRepo;
        private Topic selectedThesis;
        private Visibility visibleRestoreButton = Visibility.Hidden;
        public Visibility VisibleRestoreButton { get => visibleRestoreButton; set { visibleRestoreButton = value; OnPropertyChanged(); } }
        private int timer = 0;
        public int Timer { get => timer; set { timer = value; OnPropertyChanged(); LoadMessageRemain(); } }
        public string messageTimeRemain = "";
        public string MessageTimeRemain { get => messageTimeRemain; set { messageTimeRemain = value; OnPropertyChanged(); } }
        public Topic SelectedThesis { get => selectedThesis; set { selectedThesis = value; OnPropertyChanged(nameof(SelectedThesis)); }  }
  
        public ICommand ApproveCommand { get; set; }
        public ICommand RejectCommand { get; set; }
        public ICommand UndoCommand { get; set; }

        public ReviewThesesVM()
        {
            _thesisRepo = new ThesisRepository();
            ApproveCommand = new ViewModelCommand(ExecuteApproveCommand);
            RejectCommand = new ViewModelCommand(ExecuteRejectCommand);
            UndoCommand = new ViewModelCommand(ExecuteUndoCommand);
        }

        private void ExecuteApproveCommand(object obj)
        {
            bool canRegister = _thesisRepo.CanRegisterTopic(SelectedThesis.Id);
            if (canRegister)
            {
                //Chỗ này t định lấy danh sách sinh viên đăng kí rồi gán ThesisId của mỗi Student = Id của thesis đang đăng kí
                //Nhưng mà chưa biết lấy từ đâu của UI ;vv
            }    
                
        }

        private void ExecuteRejectCommand(object obj)
        {
            throw new NotImplementedException();
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
    }
}
