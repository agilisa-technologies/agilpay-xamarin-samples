using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace agilpay_xamarin_forms_sample.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}