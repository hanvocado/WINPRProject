using System.Windows;

namespace ThesisManagement.Views.Shared
{
    public partial class TaskProgressHistoryView : Window
    {
        public TaskProgressHistoryView()
        {
            InitializeComponent();
            taskHistoryScrollView.ScrollToBottom();
        }
    }
}
