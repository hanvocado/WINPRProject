using System.Windows;
using System.Windows.Input;
using ThesisManagement.Models;
using ThesisManagement.Repositories;
using ThesisManagement.Views.Professor;

namespace ThesisManagement.ViewModels
{
    public class TopicsVM : ViewModelBase
    {
        private readonly ITopicRepository _topicRepo;
        public IEnumerable<Topic> Topics { get; set; }

        public ICommand CreateCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public TopicsVM()
        {
            _topicRepo = new TopicRepository();
            Topics = _topicRepo.GetAll();
            CreateCommand = new ViewModelCommand(ExecuteCreateCommand);
            EditCommand = new ViewModelCommand(ExecuteEditCommand, CanExecuteEditCommand);
        }

        private bool CanExecuteEditCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteEditCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteCreateCommand(object sender)
        {
            TopicView topicView = new TopicView();
            topicView.Owner = Application.Current.MainWindow;
            topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            topicView.Show();
        }
    }
}
