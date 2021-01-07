using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using StageCompanion.Models;
using StageCompanion.Repositories.Interfaces;
using StageCompanion.Views;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    [QueryProperty(nameof(FolderId), nameof(FolderId))]
    public class FilesViewModel : BaseViewModel
    {
        private File _selectedFile;
        private readonly IFileRepository _fileRepository = DependencyService.Get<IFileRepository>();
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
            AddFileCommand = new Command(OnAddFolder);
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


        public File SelectedFile
        {
            get => _selectedFile;
            set
            {
                _selectedFile = value;
                OnFileSelected(_selectedFile);
            }
        }

        private async void OnAddFolder(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewFilePage));
        }

        async void OnFileSelected(File file)
        {
            if (file == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(FileDetailPage)}?{nameof(FileDetailViewModel.FileId)}={file.Id}");
        }
    }
}
