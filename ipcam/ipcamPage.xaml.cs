using Xamarin.Forms;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;


namespace ipcam
{
    public partial class ipcamPage : ContentPage
    {
        private readonly string[] emailsWhitelist = new[] { "anders.poulsen+adb2ctest@jayway.com", "and.poulsen@gmail.com", "anders@familien-poulsen.com", "anders.poulsen@jayway.com", "gittesommer@gmail.com", "gittenielsen1987@gmail.com" };
        
        public ipcamPage()
        {
            InitializeComponent();
        }

        //protected override async void OnAppearing()
        //{
        //    try{
        //        // Look for existing user
        //        var result = await App.AuthenticationClient.AcquireTokenSilentAsync(Constants.Scopes, GetUserByPolicy(App.AuthenticationClient.Users, Constants.SignUpSignInPolicy), Constants.Authority, false);
        //        await Navigation.PushAsync(new WebViewPage());
        //    }
        //    catch{
        //        LblWait.IsVisible = false;
        //        BtnLogIn.IsVisible = true;
        //    }
        //    base.OnAppearing();
        //}

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var results = await App.AuthenticationClient.AcquireTokenAsync(Constants.Scopes, GetUserByPolicy(App.AuthenticationClient.Users, Constants.SignUpSignInPolicy), Constants.UiParent);


                if (CheckUserInfo(results))
                {
                    LblWait.IsVisible = false;
                    await Navigation.PushAsync(new WebViewPage());
                }
                else{
                    LblWait.IsVisible = true;
                }
            }
            catch (MsalException ex)
            {
                if (ex.Message != null && ex.Message.Contains("AADB2C90118"))
                {
                    throw new Exception("Not implemented");
                }
                if (ex.ErrorCode != "authentication_canceled")
                {
                    await DisplayAlert("An error has occurred", "Exception message: " + ex.Message, "Dismiss");
                }
            }
        }

        private bool CheckUserInfo(AuthenticationResult ar){
            var user = ParseIdToken(ar.IdToken);
            var isGood = user["emails"].Any(e => emailsWhitelist.Contains(((string)e)));
            return isGood;
        }

        private JObject ParseIdToken(string idToken)
        {
            // Get the piece with actual user info
            idToken = idToken.Split('.')[1];
            idToken = Base64UrlDecode(idToken);
            return JObject.Parse(idToken);
        }

        private IUser GetUserByPolicy(IEnumerable<IUser> users, string policy)
        {
            return null;
            foreach (var user in users)
            {
                string userIdentifier = Base64UrlDecode(user.Identifier.Split('.')[0]);
                if (userIdentifier.EndsWith(policy.ToLower())) return user;
            }

            return null;
        }

        private string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }
    }
}
