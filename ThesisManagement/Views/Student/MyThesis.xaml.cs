using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThesisManagement.ViewModels;
using ThesisManagement.Views.Professor;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var thesisVM = this.DataContext as MyThesisVM;
            notificationView.DataContext = new ScheduleViewModel
            {
                ThesisId = thesisVM.Thesis.Id
            };
        }
    }
}
