using agilpay_xamarin_forms_sample.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace agilpay_xamarin_forms_sample.Views
{
    public partial class ExampleDetailPage : ContentPage
    {
        public ExampleDetailPage()
        {
            InitializeComponent();
            BindingContext = new ExampleDetailViewModel();
        }
    }
}