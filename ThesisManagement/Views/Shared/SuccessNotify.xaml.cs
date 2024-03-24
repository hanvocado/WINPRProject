namespace ThesisManagement.Views.Shared
{
    public partial class SuccessNotify
    {
        public SuccessNotify(string message)
        {
            InitializeComponent();
            txtMessage.Text = message;
        }
    }
}