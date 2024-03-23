using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ThesisManagement.Views.Student
{
    /// <summary>
    /// Interaction logic for MyTopics.xaml
    /// </summary>
    public partial class MyThesis : UserControl
    {
        public MyThesis()
        {
            YourTaskList = new ObservableCollection<_Task>
            {
                new _Task { tieude = "Thiết kế giao diện", tiendo = "80%", start = "12/03/2024",end="12/04/2024" ,trangthai="Đã hoàn thành"},
                new _Task { tieude = "Thiết kế button", tiendo = "80%", start = "12/03/2024",end="12/04/2024" ,trangthai="Đã hoàn thành"},
                new _Task { tieude = "Chạy thử", tiendo = "80%", start = "12/03/2024",end="12/04/2024" ,trangthai="Chưa hoàn thành"},
                new _Task { tieude = "khắc phục lỗi", tiendo = "80%", start = "12/03/2024",end="12/04/2024" ,trangthai="Chưa hoàn thành"},
                new _Task { tieude = "Làm báo cáo", tiendo = "80%", start = "12/03/2024",end="12/04/2024" ,trangthai="Chưa hoàn thành"},
                new _Task { tieude = "Bảo vệ luận án", tiendo = "80%", start = "12/03/2024",end="12/04/2024" ,trangthai="Chưa hoàn thành"},
                
                // Thêm các công việc khác vào đây
            };
            DataContext = this;
            InitializeComponent();
        }
        private void ListViewItem_Click(object sender, RoutedEventArgs e)
        {

        }
        public class _Task
        {
            public string tieude { get; set; }
            public string tiendo { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public string trangthai { get; set; }



        }
        public ObservableCollection<_Task> YourTaskList { get; set; }
        private void button_click(object sender, RoutedEventArgs e)
        {
            Task task = new Task();
            task.ShowDialog();

        }
    }
}
