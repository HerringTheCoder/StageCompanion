using StageCompanion.Models;
using StageCompanion.Repositories.Interfaces;
using StageCompanion.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    public class InvitationsViewModel : BaseViewModel
    {
        private readonly IInvitationRepository _invitationRepository = DependencyService.Get<IInvitationRepository>();

        public ObservableCollection<Invitation> Invitations { get; }
        public ICommand LoadInvitationsCommand { get; }
        public ICommand AcceptInvitationCommand { get; }
        public ICommand DeleteInvitationCommand { get; }

        public InvitationsViewModel()
        {
            Title = "Your invitations";
            Invitations = new ObservableCollection<Invitation>();
            LoadInvitationsCommand = new Command(async () => await ExecuteLoadInvitationsCommand());
            AcceptInvitationCommand = new Command<Invitation>(OnAcceptInvitation);
            DeleteInvitationCommand = new Command<Invitation>(OnDeclineInvitation);
        }

        private async Task ExecuteLoadInvitationsCommand()
        {
            IsBusy = true;
            try
            {
                Invitations.Clear();
                var invitations = await _invitationRepository.GetAllAsync();
                foreach (var invitation in invitations)
                {
                    Invitations.Add(invitation);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);               
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        async void OnAcceptInvitation(Invitation invitation)
        {
            if (invitation == null)
                return;
            var isSuccessful = await _invitationRepository.AcceptInvitationAsync(invitation);
            if (!isSuccessful)
            {
                var task = Shell.Current.DisplayAlert("Error", "Accepting invitation failed. Try again later.", "Ok");
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(BandPage)}?{nameof(BandViewModel.BandId)}={invitation.BandId}");
            }

        }

        private async void OnDeclineInvitation(Invitation invitation)
        {
            if (invitation == null)
                return;
            var isSuccessful = await _invitationRepository.DeleteAsync(invitation.Id.ToString());
            if (!isSuccessful)
            {
                var task = Shell.Current.DisplayAlert("Error", "Removing invitation failed. Try again later.", "Ok");
            }
        }
    }
}
