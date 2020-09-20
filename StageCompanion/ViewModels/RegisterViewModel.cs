using Newtonsoft.Json;
using PropertyChanged;
using StageCompanion.Interfaces;
using StageCompanion.Models;
using StageCompanion.Models.Responses;
using StageCompanion.Services;
using StageCompanion.Views;
using System;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class RegisterViewModel
    {
        public Command RegisterCommand { get; }
        public Command LoginCommand { get; }

        private IAuthService AuthService => DependencyService.Get<IAuthService>();

        public Credentials Credentials { get; set; }

        public Response Response { get; set; }

        public bool IsBusy { get; set; }

        public RegisterViewModel()
        {
            Credentials = new Credentials();
            Response = new Response();
            RegisterCommand = new Command(OnRegisterClicked);
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnRegisterClicked(object obj)
        {
            try
            {
                IsBusy = true;
                Response = new MessageResponse
                {
                    Message = await AuthService.Register(Credentials)
                };
                if (await AuthService.Login(Credentials))
                {
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                }
                else
                {
                    Response.Message = "Login failed. Try logging in manually.";
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

        private async void OnLoginClicked(object obj)
        {
            await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
        }


    }
}