using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ThesisManagement.ViewModels;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.Views.Shared
{
    /// <summary>
    /// Interaction logic for TasksView.xaml
    /// </summary>
    public partial class TasksView : UserControl
    {
        public TasksView()
        {
            InitializeComponent();
            PendingTaskListView.ItemContainerGenerator.StatusChanged += OnListViewItemStatusChanged;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!Window.GetWindow(this).IsActive)
                return;

            var listViewItem = sender as ListViewItem;
            var task = listViewItem?.DataContext as Task;
            TasksVM tasksVM = this.DataContext as TasksVM ?? new TasksVM();
            if (task != null)
            {
                tasksVM.Id = task.Id;
                tasksVM.ThesisId = task.ThesisId;
                tasksVM.Name = task.Name;
                tasksVM.Description = task.Description;
                tasksVM.Start = task.Start;
                tasksVM.End = task.End;
                tasksVM.Progress = task.Progress;
            }
        }

        private void OnListViewItemStatusChanged(object sender, EventArgs e)
        {
            if (PendingTaskListView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                foreach (var item in PendingTaskListView.Items)
                {
                    ListViewItem listViewItem = (ListViewItem)PendingTaskListView.ItemContainerGenerator.ContainerFromItem(item);
                    if (listViewItem != null)
                        listViewItem.MouseEnter += ListViewItem_MouseEnter;
                }
            }
        }
    }
}
