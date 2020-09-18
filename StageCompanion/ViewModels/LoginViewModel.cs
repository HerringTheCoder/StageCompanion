using PropertyChanged;
using StageCompanion.Models;
using StageCompanion.Views;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel
    {
        public Command LoginCommand { get; }

        public Credentials Credentials { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
