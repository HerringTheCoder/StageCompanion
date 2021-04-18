using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using StageCompanion.Models;
using StageCompanion.Repositories.Interfaces;
using StageCompanion.Services.Interfaces;
using StageCompanion.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    [QueryProperty(nameof(FolderId), nameof(FolderId))]
    public class FilesViewModel : BaseViewModel
    {
        private readonly IFileRepository _fileRepository = DependencyService.Get<IFileRepository>();
        private readonly IFileService _fileService = DependencyService.Get<IFileService>();
        public string FolderId { get; set; }
        public ObservableCollection<File> Files { get; }
        public Command LoadFilesCommand { get; }
        public Command AddFileCommand { get; }
        public Command<File> FileTapped { get; }

        public FilesViewModel()
        {
            Title = "Your files";
            Files = new ObservableCollection<File>();
            LoadFilesCommand = new Command(async () => await ExecuteLoadFilesCommand());
            FileTapped = new Command<File>(OnFileSelected);
            AddFileCommand = new Command(OnAddFile);
        }

        private async Task ExecuteLoadFilesCommand()
        {
            IsBusy = true;
            try
            {
                Files.Clear();
                var files = await _fileRepository.GetAllByFolderId(Convert.ToInt32(FolderId));
                foreach (var file in files)
                {
                    if (file.Extension == "jpg" || file.Extension == "jpeg" || file.Extension == "png")
                    {
                        file.FileIcon = ImageSource.FromFile("image_icon.png");
                    }
                    Files.Add(file);
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
            SelectedFile = null;
        }

        private File _selectedFile;
        public File SelectedFile
        {
            get => _selectedFile;
            set
            {
                _selectedFile = value;
                OnFileSelected(_selectedFile);
            }
        }

        private async void OnAddFile(object obj)
        {
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    await _fileService.SendFile(Convert.ToInt32(FolderId), stream, result);
                    OnAppearing();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await Shell.Current.DisplayAlert("Error,", "File upload failed. Please try again.", "Ok");
            }
        }

        async void OnFileSelected(File file)
        {
            if (file == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(FileDetailPage)}?{nameof(FileDetailViewModel.FileId)}={file.Id}");
        }
    }
}
