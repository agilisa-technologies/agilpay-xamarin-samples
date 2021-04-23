using paynet_xamarin_sample.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace paynet_xamarin_sample.Views
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