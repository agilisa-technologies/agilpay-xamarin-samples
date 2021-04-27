using System.ComponentModel;
using webpay_xamarin_sample.ViewModels;
using Xamarin.Forms;

namespace webpay_xamarin_sample.Examples
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