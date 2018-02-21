using Xamarin.Forms;
using Microsoft.Identity.Client;

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
            // Handle when your app starts
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
