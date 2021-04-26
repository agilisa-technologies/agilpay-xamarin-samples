using paynet_xamarin_sample.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace paynet_xamarin_sample.Views
{
    public partial class CardRegistrationExample : ContentPage
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public ICommand TriggerCardRegistration { get; set; }

        public CardRegistrationExample()
        {
            TriggerCardRegistration = new Command(CardRegistration);
            Title = "Card Registration Example";
            Text = "Credit Card Registration Example";
            Description = "Registering a Credit Card.";

            InitializeComponent();

            BindingContext = this;
            
            content.IsVisible = true;
            webView.IsVisible = false;
            resultContainer.IsVisible = false;
        }

        private async void CardRegistration()
        {
            try
            {
                content.IsVisible = false;
                activityIndicator.IsVisible = true;
                activityIndicator.IsRunning = true;
                Shell.SetNavBarIsVisible(this, false);

                await test();
            }
            catch (Exception ex)
            {
                activityIndicator.IsVisible = false;
                content.IsVisible = true;
                webView.IsVisible = false;
                Shell.SetNavBarIsVisible(this, true);
                result.Text = ex.Message + "\r\n" + ex.StackTrace;
                resultContainer.IsVisible = true;
            }
        }

        public async Task test()
        {
            var requestUri = "https://stage.agilpay.net/webpay6/Register?culture=en";
            var formFields = new Dictionary<string, object>
            {
                { "IsApayment", false },
                { "Invoice", "20002" },
                { "SiteId", "API-001" },
                { "UserId", "User-47748" },
                { "Identification", "00520201129" },
                { "Names", "John Smith" },
                { "Email", "j.smith@gmail.com" },
                { "Detail", "{\"Payments\":[{\"Items\":[{\"Description\":\"test\",\"Quantity\":\"1\",\"Amount\":100,\"Tax\":0}],\"MerchantKey\":\"TEST-001\",\"Service\":\"TBW9CVl7\",\"MerchantName\":\"Oriental Bank\",\"Description\":\"test\",\"Amount\":100,\"Tax\":0,\"Currency\":\"840\"}]}" },
                { "ReturnURL", "cancel" },
                { "SuccessURL", "success" },
                { "NoHeader", 1 },
                { "BodyBackground", "1" },
                { "PrimaryColor", "1" },
            };

            var authHash = await GetHash(formFields);
            formFields.Add("Authentication", authHash);

            var formElements = string.Join("",
                formFields.Select(
                  p => string.Format(@"<input type=""hidden"" value=""{0}"" name=""{1}"">",
                  p.Value.ToString().Replace("\"", "&quot;"), p.Key)));
            var html = string.Format(
                @"<html>
                <body>
                <form id=""pay"" method=""POST"" action=""{0}"">
                    {1}
                </form>
                <script type=""text/javascript"">
                    function submit()
                    {{
                        document.getElementById('pay').submit();
                    }}
                    submit();
                </script>
                </body>
            </html>", requestUri, formElements);

            webView.Navigated += (object ss, WebNavigatedEventArgs ee) =>
            {
                if (ee.Url.Contains("success"))
                {
                    result.Text = "Success";
                    resultContainer.IsVisible = true;

                    content.IsVisible = true;
                    webView.IsVisible = false;
                    Shell.SetNavBarIsVisible(this, true);
                }
                else if (ee.Url.Contains("cancel"))
                {
                    result.Text = "Cancelled";
                    resultContainer.IsVisible = true;

                    content.IsVisible = true;
                    webView.IsVisible = false;
                    Shell.SetNavBarIsVisible(this, true);
                }
            };

            EventHandler<WebNavigatedEventArgs> initialLoad = null;
            initialLoad = (s, e) =>
            {
                webView.Navigated -= initialLoad;

                activityIndicator.IsRunning = false;
                activityIndicator.IsVisible = false;
                webView.IsVisible = true;
            };
            webView.Navigated += initialLoad;

            //webView.Navigating += (object ss, WebNavigatingEventArgs ee) =>
            //{

            //};

            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = html;
            webView.Source = htmlSource;
        }

        private async Task<string> GetHash(Dictionary<string, object> formFields)
        {
            string responseBody = "";
            try
            {
                var httpClient = new HttpClient();

                var content = new MultipartFormDataContent("jfdskljfsd");
                content.Add(new StringContent(formFields["SiteId"].ToString()), "SiteId");
                content.Add(new StringContent(formFields["UserId"].ToString()), "UserId");
                content.Add(new StringContent(formFields["Identification"].ToString()), "Identification");
                content.Add(new StringContent(formFields["Detail"].ToString()), "Detail");

                HttpResponseMessage response = await httpClient.PostAsync("https://stage.agilpay.net/webpay6/api/Hash", content);
                responseBody = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                return responseBody.Trim('\\').Trim('"');
            }
            catch (Exception ex)
            {
                return $"ERR:42 | {responseBody} | {ex.Message}";
            }
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            
        }
    }
}