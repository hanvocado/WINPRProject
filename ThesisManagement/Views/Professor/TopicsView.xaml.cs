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

                this.DataContext = new TopicsVM
                {
                    Id = topic.Id,
                    Name = topic.Name,
                    ProfessorId = topic.ProfessorId,
                    StudentId = topic.StudentId,
                    Category = topic.Category,
                    Technology = topic.Technology,
                    Description = topic.Description,
                    Requirement = topic.Requirement,
                    StudentQuantity = topic.StudentQuantity,
                    Function = topic.Function
                };

                topicView.DataContext = this.DataContext;
                topicView.Owner = Application.Current.MainWindow;
                topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                topicView.Show();

                currentTopicView = topicView;
            }
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!Window.GetWindow(this).IsActive)
                return;

            var listViewItem = sender as ListViewItem;
            var topic = listViewItem?.DataContext as Topic;
            TopicsVM dataContext = this.DataContext as TopicsVM ?? new TopicsVM();
            if (topic != null)
            {
                dataContext.Id = topic.Id;
                dataContext.ProfessorId = topic.ProfessorId;
                dataContext.StudentId = topic.StudentId;
                dataContext.Name = topic.Name;
                dataContext.Category = topic.Category;
                dataContext.Technology = topic.Technology;
                dataContext.Description = topic.Description;
                dataContext.Function = topic.Function;
            }
        }

        private void OnListViewItemStatusChanged(object sender, EventArgs e)
        {
            if (TopicListView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                foreach (var item in TopicListView.Items)
                {
                    ListViewItem listViewItem = (ListViewItem)TopicListView.ItemContainerGenerator.ContainerFromItem(item);
                    if (listViewItem != null)
                        listViewItem.MouseEnter += ListViewItem_MouseEnter;
                }
            }
        }
    }
}
