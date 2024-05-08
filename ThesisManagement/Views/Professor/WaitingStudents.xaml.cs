using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ThesisManagement.Models;
using ThesisManagement.ViewModels;
using ThesisManagement.Views.Shared;

namespace ThesisManagement.Views.Professor
{
    public partial class WaitingStudents : UserControl
    {
        public WaitingStudents()
        {
            InitializeComponent();
            ThesisListView.ItemContainerGenerator.StatusChanged += OnListViewItemStatusChanged;
        }

        private void ListBoxItem_Click(object sender, RoutedEventArgs e)
        {
            var listBox = sender as ListBox;
            var selectedItem = listBox?.SelectedItem;

            if (selectedItem != null)
            {
                Thesis? thesis = selectedItem as Thesis;
                if (thesis != null)
                {
                    ProfilesView profilesView = new ProfilesView();
                    ReviewThesesVM dataContext = this.DataContext as ReviewThesesVM ?? new ReviewThesesVM();
                    if (thesis != null)
                    {
                        dataContext.SelectedThesis = new Thesis
                        {
                            Id = thesis.Id,
                            TopicId = thesis.TopicId,
                            Students = thesis.Students,
                            TopicStatus = thesis.TopicStatus,
                            File = thesis.File,
                            Score = thesis.Score
                        };
                    }

                    profilesView.DataContext = this.DataContext;
                    profilesView.Owner = Application.Current.MainWindow;
                    profilesView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    profilesView.Show();
                }

            }
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!Window.GetWindow(this).IsActive)
                return;

            var listBoxItem = sender as ListBoxItem;
            var thesis = listBoxItem?.DataContext as Thesis;
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
