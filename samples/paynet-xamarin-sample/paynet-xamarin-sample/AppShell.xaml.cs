using paynet_xamarin_sample.ViewModels;
using paynet_xamarin_sample.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace paynet_xamarin_sample
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(CardRegistrationExample), typeof(CardRegistrationExample));
            Routing.RegisterRoute(nameof(CardListExample), typeof(CardListExample));
            Routing.RegisterRoute(nameof(ProcessPaymentExample), typeof(ProcessPaymentExample));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
