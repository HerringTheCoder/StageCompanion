using System;
using System.Threading.Tasks;
using StageCompanion.ViewModels;
using StageCompanion.ViewModels.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }       
        
        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"///{nameof(RegisterPage)}");
        }
    }
}