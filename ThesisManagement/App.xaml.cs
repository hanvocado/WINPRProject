using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using ThesisManagement.Repositories.EF;
using ThesisManagement.ViewModels;
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
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>
                (options => options.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ThesisManagement; Integrated Security = True"));

            var serviceProvider = services.BuildServiceProvider();

            ProfessorMainView professorMainView = new ProfessorMainView();
            professorMainView.DataContext = serviceProvider.GetRequiredService<ProfessorMainVM>();

            var topicsView = new TopicsView();
            topicsView.DataContext = serviceProvider.GetRequiredService<TopicsVM>();

            professorMainView.Show();
        }
    }

}
