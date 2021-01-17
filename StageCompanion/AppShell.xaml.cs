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
            Routing.RegisterRoute(nameof(NewFolderPage), typeof(NewFolderPage));
            Routing.RegisterRoute(nameof(NewBandPage), typeof(NewBandPage));
            Routing.RegisterRoute(nameof(BandPage), typeof(BandPage));
            Routing.RegisterRoute(nameof(NewInvitationPage), typeof(NewInvitationPage));
            Routing.RegisterRoute(nameof(InvitationsPage), typeof(InvitationsPage));
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            SecureStorage.RemoveAll();
            await Current.GoToAsync($"///{nameof(LoginPage)}");
        }
    }
}
