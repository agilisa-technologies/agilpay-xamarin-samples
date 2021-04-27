using System;
using System.Windows.Input;
using webpay_xamarin_sample.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace webpay_xamarin_sample.ViewModels
{
    public class ConfigViewModel : BaseViewModel
    {
        private IConfigService _config => DependencyService.Get<IConfigService>();

        public ICommand GoToSamples { get; }
        public ICommand OpenWebCommand { get; }

        public string ClientId
        {
            get => _config.ClientId;
            set
            {
                _config.ClientId = value;
                OnPropertyChanged("ClientId");
            }
        }

        public string UserId
        {
            get => _config.UserId;
            set
            {
                _config.UserId = value;
                OnPropertyChanged("UserId");
            }
        }

        public string Identification
        {
            get => _config.Identification;
            set
            {
                _config.Identification = value;
                OnPropertyChanged("Identification");
            }
        }

        public ConfigViewModel()
        {
            Title = "Configuration";
            GoToSamples = new Command(async () => await Shell.Current.GoToAsync($"//examples/main"));
        }
    }
}