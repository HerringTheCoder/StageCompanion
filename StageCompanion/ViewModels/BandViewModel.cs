using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using StageCompanion.Interfaces;
using StageCompanion.Models;
using StageCompanion.Repositories.Interfaces;
using StageCompanion.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    [QueryProperty(nameof(BandId), nameof(BandId))]
    public class BandViewModel : BaseViewModel
    {
        private readonly IFileService _fileService;
        private readonly IBandRepository _bandRepository;
        public string BandId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public ObservableCollection<User> Members { get; }
        public Command AddInvitationCommand { get; }
        public Command AddBandFileCommand { get; }

        public BandViewModel()
        {
            Members = new ObservableCollection<User>();
            _bandRepository = DependencyService.Get<IBandRepository>();
            _fileService = DependencyService.Get<IFileService>();
            AddInvitationCommand = new Command(OnAddInvitation, CanExecute);
            AddBandFileCommand = new Command<User>(OnAddBandFile);
        }

        private bool _isOwner;
        public bool IsOwner
        {
            get => _isOwner;
            set
            {
                _isOwner = value;
                AddInvitationCommand.ChangeCanExecute();
            }
        }
        public bool CanExecute(object obj)
        {
            return IsOwner;
        }

        public async Task LoadBand()
        {
            try
            {
                var band = await _bandRepository.GetAsync(BandId);
                Name = $"{band.Name} members";
                Title = $"{band.Name} overview";
                Members.Clear();
                foreach (var user in band.Users)
                {
                    Members.Add(user);
                }
                if (band.LeaderId == App.CurrentUser.Id)
                {
                    IsOwner = true;
                }
                else
                {
                    IsOwner = false;
                }

            }
            catch (Exception ex)
            {
                Name = "Failed to load band";
                Debug.WriteLine(ex.Message);
            }

        }

        private async void OnAddInvitation(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewInvitationPage)}?{nameof(NewInvitationViewModel.BandId)}={BandId}");
        }

        private async void OnAddBandFile(User user)
        {
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    bool isSuccesful = await _fileService.SendBandFile(Convert.ToInt32(BandId), user.Id, stream, result);
                    if (!isSuccesful)
                    {
                        var task = Shell.Current.DisplayAlert("Error", "File upload unsuccesful", "Cancel");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }
}
