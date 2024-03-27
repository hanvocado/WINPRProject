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
            tasksView.DataContext = new TasksViewModel
            {
                ThesisId = thesisVM.Thesis.Id,
                Thesis = thesisVM.Thesis
            };
        }
    }
}
