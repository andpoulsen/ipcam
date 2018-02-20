using System;
using System.Collections.Generic;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;

namespace ipcam
{
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage()
        {
            InitializeComponent();
            this.webView.Source = new UrlWebViewSource
            {
                Url = $"https://www.familien-poulsen.com/img/?dir={DateTime.Today.ToString("yyyyMMdd")}&order=modified&sort=desc&password=8qt-wsW-hfc-GnH"
            };
            this.webView.VerticalOptions = LayoutOptions.FillAndExpand;

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        void Handle_Navigating(object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            this.LoadingLabel.IsVisible = true;
        }

        void Handle_Navigated(object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
            this.LoadingLabel.IsVisible = false;
        }

        private void backClicked(object sender, EventArgs e)
        {
            // Check to see if there is anywhere to go back to
            if (webView.CanGoBack)
            {
                webView.GoBack();
            }else{
                this.webView.Source = new UrlWebViewSource
                {
                    Url = $"https://www.familien-poulsen.com/img/?order=modified&sort=desc&password=8qt-wsW-hfc-GnH"
                };
                this.stackLayout.Children.Remove(this.webView);
                this.webView = new WebView();
                webView.Source = new UrlWebViewSource
                {
                    Url = $"https://www.familien-poulsen.com/img/?password=8qt-wsW-hfc-GnH"
                };
                webView.VerticalOptions = LayoutOptions.FillAndExpand;
                this.webView.Navigating += Handle_Navigating;
                this.webView.Navigated += Handle_Navigated;

                this.stackLayout.Children.Add(webView);
            }
        }

    }
}
