using System;
using System.Diagnostics;
using System.Threading.Tasks;
using StageCompanion.Models;
using StageCompanion.Repositories.Interfaces;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    class NewFolderViewModel : BaseViewModel
    {
        private readonly IFolderRepository _folderRepository;

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                AddFolderCommand.ChangeCanExecute();
            }
        }

        public Command AddFolderCommand { get; }
        public Command CancelCommand { get; }

        public NewFolderViewModel()
        {
            Title = "Create new folder";
            AddFolderCommand = new Command(async () => await OnSave(), ValidateSave);
            CancelCommand = new Command(OnCancel);
            _folderRepository = DependencyService.Get<IFolderRepository>();
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
            try
            {
                IsBusy = true;
                var folder = new Folder
                {
                    Name = Name,
                    OwnerId = App.CurrentUser.Id
                };

                await _folderRepository.AddAsync(folder);
                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error,", "File upload failed. Please try again.", "Ok");
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
