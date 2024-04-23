using System.Windows;
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Professor
{
    /// <summary>
    /// Interaction logic for ThesisView.xaml
    /// </summary>
    public partial class ThesisView : Window
    {
        public ThesisView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var thesisVM = this.DataContext as MyThesisVM;
            var chartVM = new ChartVM { ThesisId = thesisVM.Thesis.Id };
            mainContainer.DataContext = thesisVM;
            tasksView.DataContext = new TasksVM
            {
                ThesisId = thesisVM.Thesis.Id,
                Thesis = thesisVM.Thesis,
                ChartViewModel = chartVM
            };
            scheduleView.DataContext = new ScheduleVM
            {
                ThesisId = thesisVM.Thesis.Id
            };
            chartView.DataContext = chartVM;
            evaluationView.DataContext = thesisVM;
        }
    }
}
