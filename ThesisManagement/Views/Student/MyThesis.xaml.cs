using System.Windows.Controls;
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

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var thesisVM = this.DataContext as MyThesisVM;
            if (thesisVM != null)
            {
                tasksView.DataContext = new TasksViewModel
                {
                    ThesisId = thesisVM.Thesis.Id,
                    Thesis = thesisVM.Thesis,
                };
                notificationView.DataContext = new ScheduleViewModel
                {
                    ThesisId = thesisVM.Thesis.Id
                };
            }

        }
    }
}
