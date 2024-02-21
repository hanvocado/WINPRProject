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

namespace ThesisManagement.Views.Student
{
    /// <summary>
    /// Interaction logic for DetailFeedback.xaml
    /// </summary>
    public partial class DetailFeedback : UserControl
    {
        public DetailFeedback()
        {
            InitializeComponent();
            YourTaskList = new ObservableCollection<Task>
            {
                new Task { Time = "8:00 AM", Job = "Meeting", IsCompleted = false },
                new Task { Time = "10:00 AM", Job = "Coding", IsCompleted = true },
                // Thêm các công việc khác vào đây
            };

            // Gán danh sách công việc vào DataContext của cửa sổ
            DataContext = this;
        }
        public class Task
        {
            public string Time { get; set; }
            public string Job { get; set; }
            public bool IsCompleted { get; set; }

        }
        public ObservableCollection<Task> YourTaskList { get; set; }

    }
}
