using agilpay_xamarin_forms_sample.ViewModels;
using agilpay_xamarin_forms_sample.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using agilpay_xamarin_forms_sample.Examples;

namespace agilpay_xamarin_forms_sample
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ExampleDetailPage), typeof(ExampleDetailPage));
            Routing.RegisterRoute(nameof(ExamplesPage), typeof(ExamplesPage));
            //Routing.RegisterRoute(nameof(NewExamplePage), typeof(NewExamplePage));
            Routing.RegisterRoute(nameof(CardRegistrationExample), typeof(CardRegistrationExample));
            Routing.RegisterRoute(nameof(CardRegistrationOAuthExample), typeof(CardRegistrationOAuthExample));
            Routing.RegisterRoute(nameof(CardListExample), typeof(CardListExample));
            Routing.RegisterRoute(nameof(ProcessPaymentExample), typeof(ProcessPaymentExample));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
