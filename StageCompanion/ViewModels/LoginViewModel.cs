using PropertyChanged;
using StageCompanion.Interfaces;
using StageCompanion.Models;
using StageCompanion.Models.Responses;
using StageCompanion.Views;
using System;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class LoginViewModel
    {
        public Command LoginCommand { get; }

        private IAuthService AuthService => DependencyService.Get<IAuthService>();

        public Credentials Credentials { get; set; }

        public Response Response { get; set; }

        public bool IsBusy { get; set; }

        public LoginViewModel()
        {
            Credentials = new Credentials();
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                IsBusy = true;
                bool loginSuccessful = await AuthService.Login(Credentials);
                if (loginSuccessful)
                {
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                }
                else
                {
                    Response = new AuthorizationErrorResponse
                    {
                        Message = "Something went wrong"
                    };
                }
            }
            catch (Exception ex)
            {
                Response = new AuthorizationErrorResponse
                {
                    Message = ex.Message
                };
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
