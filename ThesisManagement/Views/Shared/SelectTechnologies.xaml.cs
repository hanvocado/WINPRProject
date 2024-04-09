using System.Windows;
using System.Windows.Controls;

namespace ThesisManagement.Views.Shared
{
    /// <summary>
    /// Interaction logic for SelectTechnologies.xaml
    /// </summary>
    public partial class SelectTechnologies : UserControl
    {
        public SelectTechnologies()
        {
            InitializeComponent();
        }

        private void TechnologyTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            TechnologiesList.IsOpen = true;
        }
    }
}
