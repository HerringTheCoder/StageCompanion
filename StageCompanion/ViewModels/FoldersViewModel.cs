using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

using StageCompanion.Models;
using StageCompanion.Repositories.Interfaces;
using StageCompanion.Views;

namespace StageCompanion.ViewModels
{
    public class FoldersViewModel : BaseViewModel
    {
        private Folder _selectedFolder;
        private readonly IFolderRepository _folderRepository = DependencyService.Get<IFolderRepository>();

        public ObservableCollection<Folder> Folders { get; }
        public Command LoadFoldersCommand { get; }
        public Command AddFolderCommand { get; }
        public Command<Folder> FolderTapped { get; }

        public FoldersViewModel()
        {
            Title = "Your folders";
            Folders = new ObservableCollection<Folder>();
            LoadFoldersCommand = new Command(async () => await ExecuteLoadFoldersCommand());
            FolderTapped = new Command<Folder>(OnFolderSelected);
            AddFolderCommand = new Command(OnAddFolder);
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


        public Folder SelectedFolder
        {
            get => _selectedFolder;
            set
            {
                _selectedFolder = value;
                OnFolderSelected(_selectedFolder);
            }
        }

        //TODO: Add NewFolder functionality
        private async void OnAddFolder(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewFilePage));
        }

        async void OnFolderSelected(Folder folder)
        {
            if (folder== null)
                return;

            // This will push the FilesPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(FilesPage)}?{nameof(FilesViewModel.FolderId)}={folder.Id}");
        }
    }
}