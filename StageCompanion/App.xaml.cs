using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StageCompanion.Services;
using StageCompanion.Views;
using StageCompanion.Interfaces;

namespace StageCompanion
{
    public partial class App : Application
    {

        public App()
        {
            Device.SetFlags(new string[] { "Brush_Experimental" });
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<IHttpService, HttpService>();
            DependencyService.Register<IAuthService, AuthService>();
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
