using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThesisManagement.Views.Professor
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentsView : UserControl
    {
        public StudentsView()
        {
            InitializeComponent();
            Topics = new ObservableCollection<AceptTopic>
            {
                new AceptTopic {Topic="Website bán hàng",Students="Võ Văn Nam, Nguyễn Bảo Hân , kha Văn Hoàng " },
            };
            this.DataContext = this;
        }
        public class AceptTopic
        {
            public string Topic { get; set; }
            public string Students { get; set; }

        }
        public ObservableCollection<AceptTopic> Topics { get; set; }
        private void Button_click(object sender, RoutedEventArgs e)
        {
            Tasks tasks = new Tasks();
            tasks.ShowDialog();
        }
    }
}
