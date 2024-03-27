using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ThesisManagement.ViewModels;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.Views.Professor
{
    /// <summary>
    /// Interaction logic for Taskss.xaml
    /// </summary>
    public partial class TasksView : UserControl
    {
        private TaskView currentTaskView;
        public static bool IsTasksViewOpen { get; set; } = false;
        public TasksView()
        {
            InitializeComponent();
            TaskListView.ItemContainerGenerator.StatusChanged += OnListViewItemStatusChanged;
        }

        private void ListViewItem_Click(object sender, RoutedEventArgs e)
        {
            var listView = sender as ListView;
            var selectedItem = listView?.SelectedItem;

            if (selectedItem != null)
            {
                Task task = selectedItem as Task;
                TaskView taskView = new TaskView();

                if (currentTaskView != null && currentTaskView.IsVisible)
                {
                    currentTaskView.Close();
                }

                this.DataContext = new TasksViewModel
                {
                    Id = task.Id,
                    ThesisId = task.ThesisId,
                    Name = task.Name,
                    Description = task.Description,
                    Start = task.Start,
                    End = task.End,
                    Progress = task.Progress
                };

                taskView.DataContext = this.DataContext;
                taskView.Owner = Application.Current.MainWindow;
                taskView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                taskView.Show();

                currentTaskView = taskView;
            }
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            //if (!Window.GetWindow(this).IsActive)
            //    return;

            var listViewItem = sender as ListViewItem;
            var task = listViewItem?.DataContext as Task;
            TasksViewModel dataContext = this.DataContext as TasksViewModel ?? new TasksViewModel();
            if (task != null)
            {
                dataContext.Id = task.Id;
                dataContext.ThesisId = task.ThesisId;
                dataContext.Name = task.Name;
                dataContext.Description = task.Description;
                dataContext.Start = task.Start;
                dataContext.End = task.End;
                dataContext.Progress = task.Progress;
            }
        }

        private void OnListViewItemStatusChanged(object sender, EventArgs e)
        {
            if (TaskListView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                foreach (var item in TaskListView.Items)
                {
                    ListViewItem listViewItem = (ListViewItem)TaskListView.ItemContainerGenerator.ContainerFromItem(item);
                    if (listViewItem != null)
                        listViewItem.MouseEnter += ListViewItem_MouseEnter;
                }
            }
        }
    }
}
