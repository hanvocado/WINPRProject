﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using Task = System.Threading.Tasks.Task;

namespace ThesisManagement.ViewModels
{
    public class ReviewThesesVM : ViewModelBase
    {
        private readonly string currentUserId;
        private readonly IThesisRepository _thesisRepo;
        private IEnumerable<Thesis> theses;
        private Thesis selectedThesis;

        public Thesis SelectedThesis { get => selectedThesis; set { selectedThesis = value; OnPropertyChanged(nameof(SelectedThesis)); } }
        public IEnumerable<Thesis> Theses { get => theses; set { theses = value; OnPropertyChanged(nameof(Theses)); } }

        private Visibility visibleRestoreButton = Visibility.Hidden;
        public Visibility VisibleRestoreButton { get => visibleRestoreButton; set { visibleRestoreButton = value; OnPropertyChanged(); } }
        private int timer = 0;
        public int Timer { get => timer; set { timer = value; OnPropertyChanged(); LoadMessageRemain(); } }
        public string messageTimeRemain = "";
        public string MessageTimeRemain { get => messageTimeRemain; set { messageTimeRemain = value; OnPropertyChanged(); } }

        public ICommand ApproveCommand { get; set; }
        public ICommand RejectCommand { get; set; }
        public ICommand UndoCommand { get; set; }

        public ReviewThesesVM()
        {
            _thesisRepo = new ThesisRepository();
            currentUserId = SessionInfo.UserId;
            selectedThesis = new Thesis();
            Theses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Waiting);
            ApproveCommand = new ViewModelCommand(ExecuteApproveCommand);
            RejectCommand = new ViewModelCommand(ExecuteRejectCommand);
            UndoCommand = new ViewModelCommand(ExecuteUndoCommand);
        }

        private void ExecuteApproveCommand(object obj)
        {
            bool canRegister = _thesisRepo.CanRegisterTopic(selectedThesis.Id);
            if (canRegister)
            {
                selectedThesis.TopicStatus = Variable.StatusTopic.Approved;
                _thesisRepo.Update(selectedThesis);
                Theses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Waiting);
            }
        }

        private void ExecuteRejectCommand(object obj)
        {
            selectedThesis.TopicStatus = Variable.StatusTopic.Rejected;
            _thesisRepo.Update(selectedThesis);
            Theses = _thesisRepo.Get(currentUserId, Variable.StatusTopic.Waiting);
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
