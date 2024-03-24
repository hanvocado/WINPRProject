namespace ThesisManagement.Views.Shared
{
    public partial class FailedNotify
    {
        public FailedNotify(string message)
        {
            InitializeComponent();
            txtMessage.Text = message;
        }
    }
}