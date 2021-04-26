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

            DependencyService.Register<MockDataStore>();
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
