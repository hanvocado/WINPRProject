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

        private Topic Topic = new Topic();
        public IEnumerable<Topic> Topics { get; set; }
        public ICommand CreateCommand { get; set; }
        public ICommand YourButtonCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public Topic selectedTopic;
        public Topic SelectedTopic
        {
            get
            {
                return selectedTopic;
            }
            set
            {
                selectedTopic = value;
                OnPropertyChanged(nameof(SelectedTopic));
            }
        }

        public TopicsVM()
        {
            selectedTopic = new Topic();
            YourButtonCommand = new ViewModelCommand(ExecuteYourButtonCommand);
            _topicRepo = new TopicRepository();
            CreateCommand = new ViewModelCommand(ExecuteCreateCommand);
            UpdateCommand = new ViewModelCommand(ExecuteUpdateCommand, CanExecuteUpdateCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            Topics = _topicRepo.GetAll();
        }

        private void ExecuteDeleteCommand(object sender)
        {
            _topicRepo.Delete(selectedTopic.Id);
        }

        private bool CanExecuteDeleteCommand(object sender)
        {
            return true;
        }

        private void ExecuteUpdateCommand(object sender)
        {
        }

        private bool CanExecuteUpdateCommand(object sender)
        {
            return true;
        }

        private void ExecuteCreateCommand(object sender)
        {
            TopicView topicView = new TopicView();
            topicView.Owner = Application.Current.MainWindow;
            topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            topicView.Show();
        }
        private void ExecuteYourButtonCommand(object parameter)
        {
            Views.Student.TopicView topicView = new Views.Student.TopicView();
            topicView.Owner = Application.Current.MainWindow;
            topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            topicView.Show();
        }

    }
}
