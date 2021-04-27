using webpay_xamarin_sample.Models;
using webpay_xamarin_sample.ViewModels;
using webpay_xamarin_sample.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace webpay_xamarin_sample.Views
{
    public partial class ExamplesPage : ContentPage
    {
        ExamplesViewModel _viewModel;

        public ExamplesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ExamplesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}