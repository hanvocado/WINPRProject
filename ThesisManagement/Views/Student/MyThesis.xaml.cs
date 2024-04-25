using System.Windows.Controls;
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Student
{
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
                var scheduleVM = new ScheduleVM { ThesisId = thesisVM.Thesis.Id };
                tasksView.DataContext = new TasksVM
                {
                    Thesis = thesisVM.Thesis,
                    ScheduleViewModel = scheduleVM
                };
                notificationView.DataContext = scheduleVM;
                professorEvaluationView.DataContext = thesisVM;
            }

        }
    }
}
