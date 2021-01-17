using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using StageCompanion.Models;
using StageCompanion.Repositories.Interfaces;
using StageCompanion.Views;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    class BandsViewModel : BaseViewModel
    {
        private readonly IBandRepository _bandRepository = DependencyService.Get<IBandRepository>();

        public ObservableCollection<Band> Bands { get; }
        public ICommand LoadBandsCommand { get; }
        public ICommand DeleteBandCommand { get; }
        public ICommand AddBandCommand { get; }

        public BandsViewModel()
        {
            Title = "Your bands";
            Bands = new ObservableCollection<Band>();
            LoadBandsCommand = new Command(async () => await ExecuteLoadBandsCommand());
            DeleteBandCommand = new Command<Band>(OnDeleteBand);
            AddBandCommand = new Command(OnAddBand);
        }

        private async Task ExecuteLoadBandsCommand()
        {
            IsBusy = true;
            try
            {
                Bands.Clear();
                var bands = await _bandRepository.GetAllRegisteredAsync();
                foreach (var band in bands)
                {
                    Bands.Add(band);
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
            SelectedBand = null;
        }

        private Band _selectedBand;
        public Band SelectedBand
        {
            get => _selectedBand;
            set
            {
                _selectedBand = value;
                OnBandSelected(value);
            }
        }

        private async void OnAddBand(object obj)
        {
            string bandName = await Shell.Current.DisplayPromptAsync("New band", "What's your band name?");
            if (!string.IsNullOrEmpty(bandName))
            {
                try
                {
                    var band = new Band
                    {
                        Name = bandName
                    };

                    await _bandRepository.AddAsync(band);
                    OnAppearing();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    await Shell.Current.DisplayAlert("Error,", "Band creation failed. Please try again later.", "Ok");
                }
            }
            //await Shell.Current.GoToAsync($"/{nameof(NewBandPage)}");
        }

        private async void OnDeleteBand(Band band)
        {
            if (band == null)
                return;
            await _bandRepository.DeleteAsync(band.Id.ToString());
            await ExecuteLoadBandsCommand();
        }

        async void OnBandSelected(Band band)
        {
            if (band == null)
                return;

            // This will push the BandPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(BandPage)}?{nameof(BandViewModel.BandId)}={band.Id}");
        }
    }
}
