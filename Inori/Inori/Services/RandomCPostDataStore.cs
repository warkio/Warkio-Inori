using Inori.Models;
using Inori.Models.randomc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inori.Services
{
    public class RandomCPostDataStore : IDataStore<RandomCPost>
    {
        readonly List<RandomCPost> items;

        public RandomCPostDataStore()
        {
            items = new List<RandomCPost>()
            {

            };
        }

        public async Task<bool> AddItemAsync(RandomCPost item)
        {
            var itemSearch = items.Where((RandomCPost arg) => arg.Id == item.Id).FirstOrDefault();
            if(itemSearch != null)
            {
                return await Task.FromResult(false);
            }
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(RandomCPost item)
        {
            var oldItem = items.Where((RandomCPost arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((RandomCPost arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<RandomCPost> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<RandomCPost>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}