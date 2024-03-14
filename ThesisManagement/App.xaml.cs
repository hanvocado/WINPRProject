using Syncfusion.Licensing;
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
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF1cXmhKYVF2WmFZfVpgfV9FYlZSQGY/P1ZhSXxXdkZiWn1fc3ZWQmBeWUE=");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShowProfessorWindow();

        }

        private void ShowProfessorWindow()
        {
            ProfessorMainView professorMainView = new();
            professorMainView.Show();
        }

        private void ShowStudentWindow()
        {
            StudentMainView wd = new();
            wd.Show();
        }
    }

}
