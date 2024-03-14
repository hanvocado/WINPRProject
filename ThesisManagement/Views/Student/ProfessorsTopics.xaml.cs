using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThesisManagement.Models;
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Student
{
    /// <summary>
    /// Interaction logic for ProfessorsTopics.xaml
    /// </summary>
    public partial class ProfessorsTopics : UserControl
    {
        private RegisterTopicView currenTopicView;

        public ProfessorsTopics()
        {
            InitializeComponent();
        }
        private void ListViewItem_Click(object sender, RoutedEventArgs e)
        {
            var listView = sender as ListView;
            var selectedItem = listView?.SelectedItem;

            if (selectedItem != null)
            {
                Topic topic = selectedItem as Topic;
                RegisterTopicView registerTopic = new RegisterTopicView();

                if (currenTopicView != null && currenTopicView.IsVisible)
                {
                    currenTopicView.Close();
                }

                this.DataContext = new TopicsViewModel
                {
                    SelectedTopic = topic
                };

                registerTopic.DataContext = this.DataContext;
                registerTopic.Owner = Application.Current.MainWindow;
                registerTopic.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                registerTopic.Show();

                currenTopicView = registerTopic;
            }
        }

    }
}
