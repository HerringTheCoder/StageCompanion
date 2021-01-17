using System;
using System.Diagnostics;
using System.Threading.Tasks;
using StageCompanion.Models;
using StageCompanion.Repositories.Interfaces;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    public class NewBandViewModel : BaseViewModel
    {
        private readonly IBandRepository _bandRepository;

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                AddBandCommand.ChangeCanExecute();
            }
        }

        public Command AddBandCommand { get; }
        public Command CancelCommand { get; }

        public NewBandViewModel()
        {
            Title = "Create new band";
            AddBandCommand = new Command(async () => await OnSave(), ValidateSave);
            CancelCommand = new Command(OnCancel);
            _bandRepository = DependencyService.Get<IBandRepository>();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrEmpty(Name);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnSave()
        {            
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
