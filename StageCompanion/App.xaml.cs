using System;
using Xamarin.Forms;
using StageCompanion.Services;
using StageCompanion.Models;
using StageCompanion.Interfaces;
using System.Threading.Tasks;
using StageCompanion.Views;
using System.Diagnostics;
using StageCompanion.Repositories;
using StageCompanion.Repositories.Interfaces;
using Xamarin.Essentials;

namespace StageCompanion
{
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }

        public App()
        {           
            InitializeComponent();
            DependencyService.Register<IHttpService, HttpService>();
            DependencyService.Register<IAuthService, AuthService>();
            DependencyService.Register<ITokenService, TokenService>();
            DependencyService.Register<IFileService, FileService>();
            DependencyService.Register<IFileRepository, FileRepository>();
            DependencyService.Register<IFolderRepository, FolderRepository>();
            DependencyService.Register<IBandRepository, BandRepository>();
            DependencyService.Register<IInvitationRepository, InvitationRepository>();
            DependencyService.Register<ISecureStorage, Storage>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            if (await CheckAuthorizationState())
                await Shell.Current.GoToAsync($"///{nameof(FoldersPage)}");
        }

        protected override async void OnResume()
        {
            if (!await CheckAuthorizationState())
                await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
        }

        protected override void OnSleep()
        {
        }

        public static async Task<bool> CheckAuthorizationState()
        {
            var tokenService = DependencyService.Get<ITokenService>();
            bool isValidated = false;
            try
            {
                isValidated = await tokenService.ValidateToken();
                string token = await SecureStorage.GetAsync("token");
                CurrentUser = tokenService.GetUserFromToken(token);
                return true;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
            }
            return isValidated;
        }
    }
}
