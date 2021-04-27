using webpay_xamarin_sample.Services;
using webpay_xamarin_sample.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace webpay_xamarin_sample
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<ExampleDataStore>();
            DependencyService.Register<ConfigService>();

            var config = DependencyService.Get<IConfigService>();
            config.ClientId = "API-001";
            config.UserId = "User-47748";
            config.Identification = "00520201129";

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
