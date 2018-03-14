using Xamarin.Forms;

// ADB2C
using Microsoft.Identity.Client;

// App center
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;


namespace ipcam
{
    public partial class App : Application
    {
        public static PublicClientApplication AuthenticationClient { get; private set; }
        
        public App()
        {
            InitializeComponent();

            AuthenticationClient = new PublicClientApplication(Constants.ClientID, Constants.Authority);
            AuthenticationClient.RedirectUri = Constants.RedirectUri;

            MainPage = new NavigationPage(new ipcamPage());
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=9953a871-2ae8-43c7-808e-62febd470d83;" +
                  "uwp={Your UWP App secret here};" +
                  "android={Your Android App secret here}",
                            typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
