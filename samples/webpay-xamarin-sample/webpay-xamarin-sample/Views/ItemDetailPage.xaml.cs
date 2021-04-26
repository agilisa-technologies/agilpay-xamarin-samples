using webpay_xamarin_sample.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace webpay_xamarin_sample.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}