using System;
using StageCompanion.Views;
using Xamarin.Essentials;
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
            SecureStorage.RemoveAll();
            await Current.GoToAsync($"///{nameof(LoginPage)}");           
        }
    }
}
