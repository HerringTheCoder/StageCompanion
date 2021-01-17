using System;
using System.Diagnostics;
using System.Threading.Tasks;
using StageCompanion.Models.Requests;
using StageCompanion.Repositories.Interfaces;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    [QueryProperty(nameof(BandId), nameof(BandId))]
    public class NewInvitationViewModel : BaseViewModel
    {
        private readonly IInvitationRepository _invitationRepository;
        public string BandId { get; set; }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                AddInvitationCommand.ChangeCanExecute();
            }
        }

        private string _role;
        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                AddInvitationCommand.ChangeCanExecute();
            }
        }

        public Command AddInvitationCommand { get; }
        public Command CancelCommand { get; }

        public NewInvitationViewModel()
        {
            Title = "Create new invitation";
            AddInvitationCommand = new Command(async () => await OnSave(), ValidateSave);
            CancelCommand = new Command(OnCancel);
            _invitationRepository = DependencyService.Get<IInvitationRepository>();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Role);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnSave()
        {
            try
            {
                IsBusy = true;
                var invitation = new InvitationRequest
                {
                    Email = _email,
                    BandId = Convert.ToInt32(BandId),
                    Role = _role
                };

                bool isSuccessful = await _invitationRepository.RequestNewAsync(invitation);
                if (!isSuccessful)
                {
                    var task = Shell.Current.DisplayAlert("Error", "Fetching invitations failed. Check your internet connection and try again.", "Ok");
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error,", "Invitation failed. Check user email again.", "Ok");
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"..?{nameof(BandViewModel.BandId)}={BandId}");
        }
    }
}
