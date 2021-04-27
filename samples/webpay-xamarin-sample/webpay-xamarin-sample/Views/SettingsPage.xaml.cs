using System;
using System.ComponentModel;
using webpay_xamarin_sample.Services;
using webpay_xamarin_sample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace webpay_xamarin_sample.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new ConfigViewModel();
        }
    }
}