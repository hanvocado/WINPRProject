using System.Windows.Controls;
using ThesisManagement.ViewModels;
using ThesisManagement.Views.Professor;

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
                tasksView.DataContext = new TasksViewModel
                {
                    ThesisId = thesisVM.Thesis.Id,
                    Thesis = thesisVM.Thesis,
                };
                notificationView.DataContext = new ScheduleVM
                {
                    ThesisId = thesisVM.Thesis.Id
                };
                professorEvaluationView.DataContext = thesisVM;
            }

        }
    }
}
