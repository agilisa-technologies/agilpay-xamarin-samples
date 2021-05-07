using System;
using System.ComponentModel;
using agilpay_xamarin_forms_sample.Services;
using agilpay_xamarin_forms_sample.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace agilpay_xamarin_forms_sample.Views
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