using Syncfusion.Licensing;
using System.Windows;
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
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF5cWWJCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXxcd3RUR2hfWUxzX0Q=");
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
