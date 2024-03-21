﻿using System.Windows;
using ThesisManagement.Views.Shared;

namespace ThesisManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ApplicationStart();
        }

        private void ApplicationStart()
        {
            var loginView = new LoginView();
            loginView.Show();
        }
    }

}
