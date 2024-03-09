using System.Windows;
using ThesisManagement.Views.Professor;
using ThesisManagement.Views.Student;

namespace ThesisManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShowStudentWindow();

        }

        private void ShowProfessorWindow()
        {
            StudentMainView StudentMainView = new();
                StudentMainView.Show();
        }

        private void ShowStudentWindow()
        {
            StudentMainView wd = new();
            wd.Show();
        }
    }

}
