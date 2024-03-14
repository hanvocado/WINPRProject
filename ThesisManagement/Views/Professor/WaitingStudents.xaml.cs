using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

            var listViewItem = sender as ListViewItem;
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
                //MessageBox.Show($"{dataContext.SelectedTopic.Id},{dataContext.SelectedTopic.Name} ,  {dataContext.SelectedTopic.Category} ,  {dataContext.SelectedTopic.Technology} ,  {dataContext.SelectedTopic.Description}");
            }
        }

        private void OnListViewItemStatusChanged(object sender, EventArgs e)
        {
            if (ThesisListView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                foreach (var item in ThesisListView.Items)
                {
                    ListViewItem listViewItem = (ListViewItem)ThesisListView.ItemContainerGenerator.ContainerFromItem(item);
                    listViewItem.MouseEnter += ListViewItem_MouseEnter;
                }
            }
        }
    }
}
