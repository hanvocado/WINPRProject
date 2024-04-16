using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ThesisManagement.Models;
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Professor
{
    /// <summary>
    /// Interaction logic for WaitingStudents.xaml
    /// </summary>
    public partial class WaitingStudents : UserControl
    {
        public WaitingStudents()
        {
            InitializeComponent();
            ThesisListView.ItemContainerGenerator.StatusChanged += OnListViewItemStatusChanged;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!Window.GetWindow(this).IsActive)
                return;

            var listViewItem = sender as ListBoxItem;
            var thesis = listViewItem?.DataContext as Thesis;
            ReviewThesesVM dataContext = this.DataContext as ReviewThesesVM ?? new ReviewThesesVM();
            if (thesis != null)
            {
                dataContext.SelectedThesis = new Thesis
                {
                    Id = thesis.Id,
                    TopicId = thesis.TopicId,
                    TopicStatus = thesis.TopicStatus,
                    File = thesis.File,
                    Score = thesis.Score
                };
            }
        }

        private void OnListViewItemStatusChanged(object sender, EventArgs e)
        {
            if (ThesisListView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                foreach (var item in ThesisListView.Items)
                {
                    var listViewItem = (ListBoxItem)ThesisListView.ItemContainerGenerator.ContainerFromItem(item);
                    if (listViewItem != null)
                        listViewItem.MouseEnter += ListViewItem_MouseEnter;
                }
            }
        }
    }
}
