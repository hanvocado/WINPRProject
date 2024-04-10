using System.Windows;
using System.Windows.Input;
using ThesisManagement.Helpers;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Professor;

namespace ThesisManagement.ViewModels
{
    public class ThesesVM : ViewModelBase
    {
        private readonly IThesisRepository _thesisRepo;

        private Thesis selectedThesis;
        public Thesis SelectedThesis
        {
            get => selectedThesis;
            set
            {
                selectedThesis = value;
                OnPropertyChanged(nameof(SelectedThesis));
            }
        }


        private IEnumerable<Thesis> approvedTheses;
        public IEnumerable<Thesis> ApprovedTheses
        {
            get => approvedTheses;
            set
            {
                approvedTheses = value;
                OnPropertyChanged(nameof(ApprovedTheses));
            }
        }

        public ICommand ShowThesisCommand { get; set; }

        public ThesesVM()
        {
            _thesisRepo = new ThesisRepository();
            ApprovedTheses = _thesisRepo.Get(SessionInfo.UserId, Variable.StatusTopic.Approved);
            ShowThesisCommand = new ViewModelCommand(ExecuteShowThesisCommand);
        }


        private void ExecuteShowThesisCommand(object obj)
        {
            var vm = new MyThesisVM();
            vm.Thesis = selectedThesis;
            vm.Topic = selectedThesis.Topic;
            var thesisView = new ThesisView { DataContext = vm };
            thesisView.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            thesisView.Show();
        }
    }
}
