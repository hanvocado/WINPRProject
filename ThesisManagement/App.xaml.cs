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
        public App()
        {
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShowStudentWindow();

        }

        private void ShowProfessorWindow()
        {
<<<<<<< HEAD
            StudentMainView StudentMainView = new();
                StudentMainView.Show();
=======
            ProfessorMainView professorMainView = new();
            professorMainView.Show();
>>>>>>> main
        }

        private void ShowStudentWindow()
        {
            StudentMainView wd = new();
            wd.Show();
        }
    }

}
