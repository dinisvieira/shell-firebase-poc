using System;
using System.Collections.Generic;
using ToDoList.ViewModels;
using ToDoList.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ToDoList
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            try
            {
                await SecureStorage.SetAsync("firebase_token",null);
                await SecureStorage.SetAsync("firebase_refresh_token",null);
                await SecureStorage.SetAsync("email",null);
                await SecureStorage.SetAsync("user_id", null);
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
