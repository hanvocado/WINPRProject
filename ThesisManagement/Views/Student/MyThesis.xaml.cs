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
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Student
{
    /// <summary>
    /// Interaction logic for MyTopics.xaml
    /// </summary>
    public partial class MyThesis : UserControl
    {
        public MyThesis()
        {
            InitializeComponent();
        }
        private void ListViewItem_Click(object sender, RoutedEventArgs e)
        {
            //var listView = sender as ListView;
            //var selectedItem = listView?.SelectedItem;
            //if (selectedItem != null)
            //{
            //    Topic topic = selectedItem as Topic;
            //    TopicView topicView = new();
            //    topicView.DataContext = new TopicVM
            //    {
            //        Name = topic.Name,
            //        Category = topic.Category,
            //        Technology = topic.Technology,
            //        Description = topic.Description
            //    };
            //    topicView.Owner = Application.Current.MainWindow;
            //    topicView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //    topicView.Show();
            }
        }
    }
