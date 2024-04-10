using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ThesisManagement.Models;
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Professor
{
    /// <summary>
    /// Interaction logic for ThesesView.xaml
    /// </summary>
    public partial class ThesesView : UserControl
    {
        public ThesesView()
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
                ((ThesesVM)this.DataContext).SelectedThesis = thesis;
            }
        }

        private void OnListViewItemStatusChanged(object sender, EventArgs e)
        {
            if (ThesisListView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                foreach (var item in ThesisListView.Items)
                {
                    ListViewItem listViewItem = (ListViewItem)ThesisListView.ItemContainerGenerator.ContainerFromItem(item);
                    if (listViewItem != null)
                        listViewItem.MouseEnter += ListViewItem_MouseEnter;
                }
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var gridView = ThesisListView.View as GridView;
            if (gridView != null)
            {
                gridView.Columns[1].Width = ThesisListView.ActualWidth - gridView.Columns[0].Width - gridView.Columns[2].Width;
            }
        }
    }
}
