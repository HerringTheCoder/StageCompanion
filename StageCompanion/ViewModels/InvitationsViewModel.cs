using StageCompanion.Models;
using StageCompanion.Repositories.Interfaces;
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
            await _invitationRepository.AcceptInvitationAsync(invitation);            

        }

        private async void OnDeclineInvitation(Invitation invitation)
        {
            if (invitation == null)
                return;
            await _invitationRepository.DeleteAsync(invitation.Id.ToString());          
        }
    }
}
