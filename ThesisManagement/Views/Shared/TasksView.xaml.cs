using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ThesisManagement.ViewModels;
using Task = ThesisManagement.Models.Task;

namespace ThesisManagement.Views.Shared
{
    public partial class TasksView : UserControl
    {
        private TaskView currentTaskView;
        public TasksView()
        {
            InitializeComponent();
            PendingTaskListView.ItemContainerGenerator.StatusChanged += OnListViewItemStatusChanged;
            DoneTaskListView.ItemContainerGenerator.StatusChanged += OnListViewItemStatusChanged;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!Window.GetWindow(this).IsActive)
                return;

            var listViewItem = sender as ListBoxItem;
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
                    ListBoxItem task = (ListBoxItem)PendingTaskListView.ItemContainerGenerator.ContainerFromItem(item);
                    if (task != null)
                        task.MouseEnter += ListViewItem_MouseEnter;
                }
            }
        }
    }
}
