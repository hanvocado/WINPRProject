using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
                TopicView topicView = new TopicView();

                TopicVM topicVM = new TopicVM
                {
                    SelectedTopic = new Topic
                    {
                        Name = topic.Name,
                        Category = topic.Category,
                        Technology = topic.Technology,
                        Description = topic.Description
                    }
                };
               // ShowMessage($"{topicVM.SelectedTopic.Name}, {topicVM.SelectedTopic.Category},{topicVM.SelectedTopic.Technology},{topicVM.SelectedTopic.Description}");

                topicView.DataContext = topicVM;
                topicView.Owner = Application.Current.MainWindow;
                topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                topicView.Show();
            }
        }

        private void FilterTexbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TopicListView.Items.Filter = FilterMethod;
        }

        private bool FilterMethod(object obj)
        {
            var topic = (Topic)obj;
            return topic.Name.Contains(FilterTexbox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            var topic = listViewItem?.DataContext as Topic;
            if (topic != null)
            {
                TopicVM topicVM = new TopicVM
                {
                    SelectedTopic = new Topic
                    {
                        Id = topic.Id,
                        Name = topic.Name,
                        Category = topic.Category,
                        Technology = topic.Technology,
                        Description = topic.Description
                    }
                };
               //MessageBox.Show($"{topicVM.SelectedTopic.Id},{topicVM.SelectedTopic.Name} ,  {topicVM.SelectedTopic.Category} ,  {topicVM.SelectedTopic.Technology} ,  {topicVM.SelectedTopic.Description}");
            }
        }
    }
}
