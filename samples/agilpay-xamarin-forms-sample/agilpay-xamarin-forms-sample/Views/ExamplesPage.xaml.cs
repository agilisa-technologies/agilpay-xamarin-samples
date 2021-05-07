using agilpay_xamarin_forms_sample.Models;
using agilpay_xamarin_forms_sample.ViewModels;
using agilpay_xamarin_forms_sample.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace agilpay_xamarin_forms_sample.Views
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