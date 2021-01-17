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
    public class FoldersViewModel : BaseViewModel
    {
        private readonly IFolderRepository _folderRepository = DependencyService.Get<IFolderRepository>();

        public ObservableCollection<Folder> Folders { get; }
        public ICommand LoadFoldersCommand { get; }
        public ICommand AddFolderCommand { get; }
        public ICommand DeleteFolderCommand { get; }

        public FoldersViewModel()
        {
            Title = "Your folders";
            Folders = new ObservableCollection<Folder>();
            LoadFoldersCommand = new Command(async () => await ExecuteLoadFoldersCommand());
            AddFolderCommand = new Command(OnAddFolder);
            DeleteFolderCommand = new Command<Folder>(OnDeleteFolder);
        }

        private async Task ExecuteLoadFoldersCommand()
        {
            IsBusy = true;
            try
            {
                Folders.Clear();
                var folders = await _folderRepository.GetAllAsync();
                foreach (var folder in folders)
                {
                    Folders.Add(folder);
                }
                Folders.Clear();
                foreach (var folder in folders)
                {
                    Folders.Add(folder);
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
            SelectedFolder = null;
        }

        private Folder _selectedFolder;
        public Folder SelectedFolder
        {
            get => _selectedFolder;
            set
            {
                _selectedFolder = value;
                OnFolderSelected(value);
            }
        }

        private async void OnAddFolder(object obj)
        {
            string folderName = await Shell.Current.DisplayPromptAsync("New folder", "Type in folder name");
            if (!string.IsNullOrEmpty(folderName))
            {
                try
                {
                    var folder = new Folder
                    {
                        Name = folderName,
                        OwnerId = App.CurrentUser.Id
                    };

                    await _folderRepository.AddAsync(folder);
                    OnAppearing();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    await Shell.Current.DisplayAlert("Error,", "File upload failed. Please try again.", "Ok");
                }
            }
            //await Shell.Current.GoToAsync($"/{nameof(NewFolderPage)}");
        }

        private async void OnDeleteFolder(Folder folder)
        {
            if (folder == null)
                return;
            await _folderRepository.DeleteAsync(folder.Id.ToString());
            await ExecuteLoadFoldersCommand();
        }

        async void OnFolderSelected(Folder folder)
        {
            if (folder == null)
                return;

            // This will push the FilesPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(FilesPage)}?{nameof(FilesViewModel.FolderId)}={folder.Id}");
        }
    }
}