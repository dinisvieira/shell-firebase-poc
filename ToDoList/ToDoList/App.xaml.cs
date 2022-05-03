﻿using System;
using ToDoList.Services;
using ToDoList.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ToDoList
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            DependencyService.Register<ItemDataStore>();
            DependencyService.Register<AuthenticationService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
