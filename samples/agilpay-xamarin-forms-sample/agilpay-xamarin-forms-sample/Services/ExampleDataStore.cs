using agilpay_xamarin_forms_sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agilpay_xamarin_forms_sample.Services
{
    public class ExampleDataStore : IItemsDataStore<Example>
    {
        readonly List<Example> items;

        public ExampleDataStore()
        {
            items = new List<Example>()
            {
                new Example { Id = Guid.NewGuid().ToString(), Title = "Credit Card Registration Example", Description="Registering a Credit Card.", ExampleName = "CardRegistrationExample" },
                new Example { Id = Guid.NewGuid().ToString(), Title = "Credit Card Registration Example (OAuth Flow)", Description="Registering a Credit Card. (New OAuth flow).", ExampleName = "CardRegistrationOAuthExample" },
                new Example { Id = Guid.NewGuid().ToString(), Title = "Card List", Description="Getting a list of Credit Cards and their respective Tokens.", ExampleName = "CardListExample" },
                new Example { Id = Guid.NewGuid().ToString(), Title = "Process a Payment", Description="Example on how to use a Token to process a payment.", ExampleName = "ProcessPaymentExample" },
            };
        }

        public async Task<bool> AddItemAsync(Example item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Example item)
        {
            var oldItem = items.Where((Example arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Example arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Example> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Example>> GetItemAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}