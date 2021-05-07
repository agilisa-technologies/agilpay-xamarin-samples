using System.ComponentModel;
using agilpay_xamarin_forms_sample.ViewModels;
using Xamarin.Forms;

namespace agilpay_xamarin_forms_sample.Examples
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