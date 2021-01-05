using System;
using Xamarin.Forms;
using StageCompanion.Services;
using StageCompanion.Models;
using StageCompanion.Interfaces;
using System.Threading.Tasks;
using StageCompanion.Views;
using System.Diagnostics;
using StageCompanion.Repositories;

namespace StageCompanion
{
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }

        public App()
        {
            Device.SetFlags(new string[] { "Brush_Experimental" });
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IHttpService, HttpService>();
            DependencyService.Register<IAuthService, AuthService>();
            DependencyService.Register<ITokenService, TokenService>();
            DependencyService.Register<IFileService, FileService>();
            DependencyService.Register<IDataStore<File>, FileRepository>();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            if (await CheckAuthorization())
                await Shell.Current.GoToAsync($"///{nameof(ItemsPage)}");
            else
                await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
        }

        protected override async void OnResume()
        {
            await CheckAuthorization();
        }

        protected override void OnSleep()
        {
        }

        private async Task<bool> CheckAuthorization()
        {
            var TokenService = DependencyService.Get<ITokenService>();
            bool isValidated = false;
            try
            {
                isValidated = await TokenService.ValidateToken();
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
