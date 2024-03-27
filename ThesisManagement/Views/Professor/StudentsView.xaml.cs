using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ThesisManagement.Models;
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Professor
{
    public partial class StudentsView : UserControl
    {
        public StudentsView()
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
            if (thesis != null)
            {
                ((ReviewThesesVM)this.DataContext).SelectedThesis = thesis;
                //MessageBox.Show($"{dataContext.ThesisId}" );
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
