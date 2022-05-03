using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using ToDoList.Models;
using Xamarin.Essentials;

namespace ToDoList.Services
{
    internal class ItemDataStore : IDataStore<Item>
    {
        public static string FirebaseClient = "https://todolist-e2543-default-rtdb.firebaseio.com/";

        public FirebaseClient fc = new FirebaseClient(FirebaseClient, new FirebaseOptions { AuthTokenAsyncFactory = GetToken });
        private static string _userId;

        private static async Task<string> GetToken()
        {
            try
            {
                var firebaseToken = await SecureStorage.GetAsync("firebase_token");
                var refreshToken = await SecureStorage.GetAsync("firebase_refresh_token");
                _userId = await SecureStorage.GetAsync("user_id");
                if (!string.IsNullOrWhiteSpace(firebaseToken) && !string.IsNullOrWhiteSpace(refreshToken))
                {
                    return firebaseToken;
                }
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            var json = JsonConvert.SerializeObject(item);
            await fc.Child("ItemTable").Child(_userId).Child(item.Id).PutAsync(json);

            return true;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var json = JsonConvert.SerializeObject(item);
            await fc.Child("ItemTable").Child(_userId).Child(item.Id).PutAsync(json);

            return true;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            await fc.Child("ItemTable").Child(_userId).Child(id).DeleteAsync();

            return true;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            var item = await fc.Child("ItemTable").Child(_userId).Child(id).OnceSingleAsync<Item>();
            return item;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                var getItems = (await fc
                    .Child("ItemTable")
                    .Child(_userId)
                    .OnceAsync<Item>()).Select(item => new Item
                {
                    Description = item.Object.Description,
                    Text = item.Object.Text, 
                    Id = item.Key
                });

                return getItems;
            }
            catch (FirebaseException ex)
            {
                Debug.WriteLine(ex);
                return new List<Item>();
            }
        }
    }
}
