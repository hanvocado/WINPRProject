using System.Windows;
using System.Windows.Controls;
using ThesisManagement.Models;
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Student
{
    /// <summary>
    /// Interaction logic for ProfessorsTopics.xaml
    /// </summary>
    public partial class ProfessorsTopics : UserControl
    {
        private RegisterTopicView currenTopicView;

        public ProfessorsTopics()
        {
            InitializeComponent();
        }
        private void ListViewItem_Click(object sender, RoutedEventArgs e)
        {
            var listView = sender as ListView;
            var selectedItem = listView?.SelectedItem;

            if (selectedItem != null)
            {
                Topic topic = selectedItem as Topic;
                RegisterTopicView registerTopic = new RegisterTopicView();

                if (currenTopicView != null && currenTopicView.IsVisible)
                {
                    currenTopicView.Close();
                }

                this.DataContext = new TopicsViewModel
                {
                    Id = topic.Id,
                    Name = topic.Name,
                    ProfessorId = topic.ProfessorId,
                    StudentId = topic.StudentId,
                    Category = topic.Category,
                    Technology = topic.Technology,
                    Description = topic.Description,
                    Requirement = topic.Requirement,
                    StudentQuantity = topic.StudentQuantity
                };

                registerTopic.DataContext = this.DataContext;
                registerTopic.Owner = Application.Current.MainWindow;
                registerTopic.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                registerTopic.Show();

                currenTopicView = registerTopic;
            }
        }

    }
}
