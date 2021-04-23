using paynet_xamarin_sample.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace paynet_xamarin_sample.Views
{
    public partial class ProcessPaymentExample : ContentPage
    {
        public ProcessPaymentExample()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}