using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private TopicView currentTopicView;
        public static bool IsTopicViewOpen { get; set; } = false;
        public TopicsView()
        {
            InitializeComponent();
            TopicListView.ItemContainerGenerator.StatusChanged += OnListViewItemStatusChanged;
        }

        private void ListViewItem_Click(object sender, RoutedEventArgs e)
        {
            var listView = sender as ListView;
            var selectedItem = listView?.SelectedItem;

            if (selectedItem != null)
            {
                Topic topic = selectedItem as Topic;
                TopicView topicView = new TopicView();

                if (currentTopicView != null && currentTopicView.IsVisible)
                {
                    currentTopicView.Close();
                }

                this.DataContext = new TopicsViewModel
                {
                    SelectedTopic = topic
                };
                //MessageBox.Show($"{topicVM.SelectedTopic.Id}, {topicVM.SelectedTopic.Name}, {topicVM.SelectedTopic.Category},{topicVM.SelectedTopic.Technology},{topicVM.SelectedTopic.Description}");

                topicView.DataContext = this.DataContext;
                topicView.Owner = Application.Current.MainWindow;
                topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                topicView.Show();

                currentTopicView = topicView;
            }
        }

        //private void FilterTexbox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    TopicListView.Items.Filter = FilterMethod;
        //}

        //private bool FilterMethod(object obj)
        //{
        //    var topic = (Topic)obj;
        //    return topic.Name.Contains(FilterTextbox.Text, StringComparison.OrdinalIgnoreCase);
        //}

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!Window.GetWindow(this).IsActive)
                return;

            var listViewItem = sender as ListViewItem;
            var topic = listViewItem?.DataContext as Topic;
            TopicsViewModel dataContext = this.DataContext as TopicsViewModel ?? new TopicsViewModel();
            if (topic != null)
            {
                dataContext.SelectedTopic = new Topic
                {
                    Id = topic.Id,
                    ProfessorId = topic.ProfessorId,
                    StudentId = topic.StudentId,
                    Name = topic.Name,
                    Category = topic.Category,
                    Technology = topic.Technology,
                    Description = topic.Description
                };
                //MessageBox.Show($"{dataContext.SelectedTopic.Id},{dataContext.SelectedTopic.Name} ,  {dataContext.SelectedTopic.Category} ,  {dataContext.SelectedTopic.Technology} ,  {dataContext.SelectedTopic.Description}");
            }
        }

        private void OnListViewItemStatusChanged(object sender, EventArgs e)
        {
            if (TopicListView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                foreach (var item in TopicListView.Items)
                {
                    ListViewItem listViewItem = (ListViewItem)TopicListView.ItemContainerGenerator.ContainerFromItem(item);
                    listViewItem.MouseEnter += ListViewItem_MouseEnter;
                }
            }
        }
    }
}
