using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using agilpay_xamarin_forms_sample.Services;
using agilpay_xamarin_forms_sample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace agilpay_xamarin_forms_sample.Examples
{
    public partial class CardRegistrationExample : ContentPage
    {
        private IConfigService _config => DependencyService.Get<IConfigService>();
        public ICommand TriggerCardRegistration { get; set; }

        public string ClientId => _config.ClientId;
        public string UserId => _config.UserId;
        public string Identification => _config.Identification;

        public CardRegistrationExample()
        {
            TriggerCardRegistration = new Command(CardRegistration);

            InitializeComponent();

            BindingContext = new ExampleDetailViewModel();
            
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
                resultContainer.IsVisible = false;
                Shell.SetNavBarIsVisible(this, false);

                await StartWebViewFlow();
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

        /// <summary>
        /// In this method the WebView is set up for registration fo a credit card.
        /// </summary>
        /// <returns></returns>
        public async Task StartWebViewFlow()
        {
            var requestUri = "https://stage.agilpay.net/webpay6/Register?culture=en";

            // Set the formFileds that will be added to the POST request.
            var formFields = new Dictionary<string, object>
            {
                { "IsApayment", false },
                { "Invoice", "20002" },
                { "SiteId", _config.ClientId },
                { "UserId", _config.UserId },
                { "Identification", _config.Identification },
                { "Names", "John Smith" },
                { "Email", "j.smith@gmail.com" },
                { "Detail", "{\"Payments\":[{\"Items\":[{\"Description\":\"test\",\"Quantity\":\"1\",\"Amount\":100,\"Tax\":0}],\"MerchantKey\":\"TEST-001\",\"Service\":\"TBW9CVl7\",\"MerchantName\":\"Oriental Bank\",\"Description\":\"test\",\"Amount\":100,\"Tax\":0,\"Currency\":\"840\"}]}" },
                { "ReturnURL", "https://stage.agilpay.net/webpay6/Cancel" },
                { "SuccessURL", "https://stage.agilpay.net/webpay6/Success" },
                { "NoHeader", 1 },
                { "BodyBackground", "1" },
                { "PrimaryColor", "1" },
                { "ShowWallet", false } // Hides the wallet (current cards) when registering a card.
            };

            // We get the auth hash by a web request.
            var authHash = await GetHash(formFields);
            formFields.Add("Authentication", authHash);

            // Generate HTML that will be initially rendered.
            // Notice the javascript function "submit" is called as soon as the js code is parsed on the WebView.
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

            // We listen to the Navigated event in order to identify when we reach the end of our flow.
            // We know we are at the end by matching to the `ReturnURL` and `SuccessURL` specified above.
            webView.Navigated += async (object ss, WebNavigatedEventArgs ee) =>
            {
                if (ee.Url.ToLower().Contains("success"))
                {
                    result.Text = "Success";
                    resultContainer.IsVisible = true;

                    content.IsVisible = true;
                    webView.IsVisible = false;
                    Shell.SetNavBarIsVisible(this, true);

                    var token = await GetTokenAfterRegistration();
                    result.Text += "\n\nToken:\n" + token;
                }
                else if (ee.Url.ToLower().Contains("cancel"))
                {
                    result.Text = "Cancelled";
                    resultContainer.IsVisible = true;

                    content.IsVisible = true;
                    webView.IsVisible = false;
                    Shell.SetNavBarIsVisible(this, true);
                }
            };

            // This event handler is used to know when the page initially loads.
            // Notice it self removes itself after the first event call.
            EventHandler<WebNavigatedEventArgs> initialLoad = null;
            initialLoad = (s, e) =>
            {
                webView.Navigated -= initialLoad;

                activityIndicator.IsRunning = false;
                activityIndicator.IsVisible = false;
                webView.IsVisible = true;
            };
            webView.Navigated += initialLoad;

            // We create the HtmlWebViewSource and pass in the HTML we've generated above.
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = html;
            webView.Source = htmlSource;
        }

        /// <summary>
        /// This method requests the Authentication HASH by consuming an HTTP endpoint.
        /// </summary>
        /// <param name="formFields"></param>
        /// <returns></returns>
        private async Task<string> GetHash(Dictionary<string, object> formFields)
        {
            var httpClient = new HttpClient();

            var content = new MultipartFormDataContent("jfdskljfsd");
            content.Add(new StringContent(formFields["SiteId"].ToString()), "SiteId");
            content.Add(new StringContent(formFields["UserId"].ToString()), "UserId");
            content.Add(new StringContent(formFields["Identification"].ToString()), "Identification");
            content.Add(new StringContent(formFields["Detail"].ToString()), "Detail");

            HttpResponseMessage response = await httpClient.PostAsync("https://stage.agilpay.net/webpay6/api/Hash", content);
            var responseBody = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return responseBody.Trim('\\').Trim('"');
        }

        /// <summary>
        /// Gets the token from the success page by calling the mobile_callback javascipt function which returns the
        /// JSON document with the result information. More details at: TODO: Document this.
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetTokenAfterRegistration()
        {

            var registrationResult = await webView.EvaluateJavaScriptAsync("mobile_callback()");
            // Example of the document returned by the mobile_callback() javascript method.
            //
            //{
            //    "Response": {
            //        "MerchantKey": "API-001",
            //        "Service": "TBW9CVl7",
            //        "MerchantName": "API TESTS",
            //        "Description": "test",
            //        "Amount": "100",
            //        "Currency": "840",
            //        "UseRecurring": null,
            //        "RecurringPeriod": "0",
            //        "RecurringFrequency": "0",
            //        "RecurringDay": "0",
            //        "RecurringQty": "0",
            //        "RecurringAmount": "0",
            //        "RecurringFromDate": null,
            //        "RecurringToDate": null,
            //        "ResponseCode": "00",
            //        "Message": "Success",
            //        "PAN": null,
            //        "Token": "4341299b5f2eaac55459d8b4910bb14380558"
            //    }
            //}

            // In this example in particular the returned JSON payload
            // had some unwanted characters. This will probably be fixed
            // in the future, but for now we need to clean it up a bit before parsing.
            var cleanedRegistrationResult = registrationResult
                    .Replace("\\n", "")
                    .Replace("\n", "")
                    .Replace("\\", "")
                    .Replace(" ", "");

            // We parse the returned JSON document string. This can be done
            // any way desired. It's just a standard JSON Deserialization.
            var parsed = JObject.Parse(cleanedRegistrationResult);
            var response = (JObject)parsed["Response"];
            var token = (string)response["Token"];

            // And we return the token.
            return token;
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            
        }

        /// <summary>
        /// This method enriches the back button behavior.
        /// When the webView is visible (the webView flow is active), the back button hides
        /// the webView but doesn't do a back to the navigation on the Shell/Xamarin navigation stack.
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            if (webView.IsVisible)
            {
                content.IsVisible = true;
                webView.IsVisible = false;
                Shell.SetNavBarIsVisible(this, true);
                return true;
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }
    }
}