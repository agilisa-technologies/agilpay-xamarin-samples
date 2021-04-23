using paynet_xamarin_sample.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace paynet_xamarin_sample.Views
{
    public partial class CardRegistrationExample : ContentPage
    {
        public ICommand TriggerCardRegistration { get; set; }

        public CardRegistrationExample()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();

            content.IsVisible = true;
            webView.IsVisible = true;

            TriggerCardRegistration = new Command(CardRegistration);
        }

        private async void CardRegistration()
        {
            var result = await GetHash();

            //content.IsVisible = false;
            //webView.IsVisible = true;
        }

        public async void test()
        {
            var requestUri = "";
            var formFields = new Dictionary<string, string>
            {
                { "SiteID", "API-001" },
                { "UserID", "User-47748" },
                { "Identification", ""}
            };

            var formElements = string.Join("",
                formFields.Select(
                  p => string.Format(@"<input type=""hidden"" value=""{0}"" name=""{1}"">",
                  p.Value, p.Key)));
                        var html = string.Format(
                          @"<html>
                          <head>
                            <script type=""text/javascript"">
                              function submit()
                              {{
                                document.getElementById('pay').submit();
                              }}
                            </script>
                          </head>
                          <body>
                            <form id=""pay"" method=""POST"" action=""{0}"">
                              {1}
                            </form>
                          </body>
                        </html>", requestUri, formElements);

            EventHandler<WebNavigatedEventArgs> loaded = null;
            loaded = (s, e) =>
            {
                webView.Navigated -= loaded;
                webView.EvaluateJavaScriptAsync("submit()");
            };
            webView.Navigated += loaded;

            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = html;
            webView.Source = htmlSource;
        }

        private async Task<string> GetHash()
        {
            try
            {
                var httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync("https://stage.agilpay.net/webpay6/api/Hash");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception)
            {
                return "ERR:42";
            }
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            
        }
    }
}