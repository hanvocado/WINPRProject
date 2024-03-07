using System.Windows;
using ThesisManagement.Views.Professor;

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

            ShowProfessorWindow();
        }

        private void ShowProfessorWindow()
        {
            ProfessorMainView professorMainView = new ProfessorMainView();
            professorMainView.Show();
        }
    }

}
