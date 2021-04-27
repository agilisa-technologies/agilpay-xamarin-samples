using System.ComponentModel;
using webpay_xamarin_sample.ViewModels;
using Xamarin.Forms;

namespace webpay_xamarin_sample.Examples
{
    public partial class ProcessPaymentExample : ContentPage
    {
        public ProcessPaymentExample()
        {
            InitializeComponent();
            BindingContext = new ExampleDetailViewModel();
        }
    }
}