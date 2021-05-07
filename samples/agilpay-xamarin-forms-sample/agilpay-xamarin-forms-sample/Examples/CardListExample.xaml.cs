using System.ComponentModel;
using agilpay_xamarin_forms_sample.ViewModels;
using Xamarin.Forms;

namespace agilpay_xamarin_forms_sample.Examples
{
    public partial class CardListExample : ContentPage
    {
        public CardListExample()
        {
            InitializeComponent();
            BindingContext = new ExampleDetailViewModel();
        }
    }
}