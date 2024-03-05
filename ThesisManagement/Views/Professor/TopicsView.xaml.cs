using System.Windows;
using System.Windows.Controls;
using ThesisManagement.Models;
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Professor
{
    /// <summary>
    /// Interaction logic for TopicView.xaml
    /// </summary>
    public partial class TopicsView : UserControl
    {
        public TopicsView()
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
                TopicView topicView = new();
                topicView.DataContext = new TopicVM
                {
                    Name = topic.Name,
                    Category = topic.Category,
                    Technology = topic.Technology,
                    Description = topic.Description
                };
                topicView.Owner = Application.Current.MainWindow;
                topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                topicView.Show();
            }
        }
    }
}
