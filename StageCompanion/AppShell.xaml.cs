using System;
using System.Collections.Generic;
using StageCompanion.ViewModels;
using StageCompanion.Views;
using Xamarin.Forms;

namespace StageCompanion
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));          
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {            
            await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");            
        }
    }
}
