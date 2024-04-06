using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ThesisManagement.Views.Shared
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var defaultButton = FocusManager.GetFocusedElement(this) as Button;
                if (defaultButton != null)
                {
                    var command = defaultButton.Command;
                    if (command != null && command.CanExecute(null))
                    {
                        command.Execute(null);
                        e.Handled = true;
                    }
                }
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
