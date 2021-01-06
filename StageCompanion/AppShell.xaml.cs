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
            Routing.RegisterRoute(nameof(FilesPage), typeof(FilesPage));
            Routing.RegisterRoute(nameof(FileDetailPage), typeof(FileDetailPage));
            Routing.RegisterRoute(nameof(NewFilePage), typeof(NewFilePage));
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll();
            await Current.GoToAsync($"///{nameof(LoginPage)}");
        }
    }
}
