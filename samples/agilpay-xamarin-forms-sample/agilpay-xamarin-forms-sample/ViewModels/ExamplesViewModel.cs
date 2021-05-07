using agilpay_xamarin_forms_sample.Models;
using agilpay_xamarin_forms_sample.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace agilpay_xamarin_forms_sample.ViewModels
{
    public class ExamplesViewModel : BaseViewModel
    {
        private Example _selectedItem;

        public ObservableCollection<Example> Examples { get; }
        public Command LoadExamplesCommand { get; }
        public Command<Example> ExampleTapped { get; }

        public ExamplesViewModel()
        {
            Title = "AgilPay Xamarin Examples";
            Examples = new ObservableCollection<Example>();
            LoadExamplesCommand = new Command(async () => await ExecuteLoadExamplesCommand());

            ExampleTapped = new Command<Example>(OnItemSelected);
        }

        async Task ExecuteLoadExamplesCommand()
        {
            IsBusy = true;

            try
            {
                Examples.Clear();
                var items = await DataStore.GetItemAsync(true);
                foreach (var item in items)
                {
                    Examples.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Example SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Example item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{item.ExampleName}?{nameof(ExampleDetailViewModel.ItemId)}={item.Id}");
        }
    }
}