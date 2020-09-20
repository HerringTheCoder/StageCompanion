using System;
using Xamarin.Forms;
using StageCompanion.Services;
using StageCompanion.Models;
using StageCompanion.Interfaces;
using Xamarin.Essentials;
using System.Threading.Tasks;
using StageCompanion.Views;
using System.Diagnostics;

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
            MainPage = new AppShell();
        }

        protected override async void OnStart()
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
            if (isValidated)
                await Shell.Current.GoToAsync($"///{nameof(ItemsPage)}");
            else
                await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
        }

        protected override void OnSleep()
        {
        }

        protected override async void OnResume()
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
            if (!isValidated)
            {
                await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
            }
        }
    }
}
