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
using ThesisManagement.ViewModels;

namespace ThesisManagement.Views.Student
{
    /// <summary>
    /// Interaction logic for MyTopics.xaml
    /// </summary>
    public partial class MyThesis : UserControl
    {
        public MyThesis()
        {
            InitializeComponent();
            YourTaskList = new ObservableCollection<Task>
            {
                new Task { tieude = "Thiết kế giao diện", tiendo = "80%", start = "12/03/2024",end="12/04/2024" ,trangthai="Đã hoàn thành"},
                new Task { tieude = "Thiết kế button", tiendo = "80%", start = "12/03/2024",end="12/04/2024" ,trangthai="Đang hoàn thành"},
                new Task { tieude = "Chạy thử", tiendo = "80%", start = "12/03/2024",end="12/04/2024" ,trangthai="Đang thực hiện"},
                new Task { tieude = "khắc phục lỗi", tiendo = "80%", start = "12/03/2024",end="12/04/2024" ,trangthai="Đang thực hiện"},
                new Task { tieude = "Làm báo cáo", tiendo = "80%", start = "12/03/2024",end="12/04/2024" ,trangthai="Đang thực hiện"},
                new Task { tieude = "Bảo vệ luận án", tiendo = "80%", start = "12/03/2024",end="12/04/2024" ,trangthai="Đang thực hiện"},
                
                // Thêm các công việc khác vào đây
            };
            DataContext = this;
        }
        private void ListViewItem_Click(object sender, RoutedEventArgs e)
        {

        }
        public class Task
        {
            public string tieude { get; set; }
            public string tiendo { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string trangthai { get; set; }
            
            

        }
        public ObservableCollection<Task> YourTaskList { get; set; }
        private void button_click(object sender, RoutedEventArgs e)
        {
            
            
        }
    }
}
