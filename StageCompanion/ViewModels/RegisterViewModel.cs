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

        private IAuthService AuthService => DependencyService.Get<IAuthService>();

        public Credentials Credentials { get; set; }

        public Response Response { get; set; }

        public RegisterViewModel()
        {
            Credentials = new Credentials();
            Response = new Response();
            RegisterCommand = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked(object obj)
        {            
            try
            {
                Response = new MessageResponse
                {
                    Message = await AuthService.Register(Credentials)
                };
            }
            catch (Exception ex)
            {
                Response = new AuthorizationErrorResponse
                {
                    Message = ex.Message
                };
            };
            //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");

            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one            
        }
    }
}