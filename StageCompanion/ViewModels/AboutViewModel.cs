using PropertyChanged;
using StageCompanion.Interfaces;
using StageCompanion.Models;
using FileModel = StageCompanion.Models.File;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StageCompanion.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AboutViewModel : BaseViewModel
    {
        public ICommand OpenWebCommand { get; }
        public ICommand PickImage { get; }
        public User User { get; set; }
        public string Text { get; set; }
        public ImageSource Source { get; set; }
        private IDataStore<FileModel> FileRepository => DependencyService.Get<IDataStore<FileModel>>();

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            PickImage = new Command(async () => await PickAndShow());
            User = App.CurrentUser;
        }

        public async Task PickAndShow(PickOptions options = null)
        {
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    Text = $"File Name: {result.FileName}";
                    if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        var source = ImageSource.FromStream(() => stream);
                        Source = source;

                        var fileName = result.FileName.Split('.');
                        byte[] imageArray = null;
                        using (var memoryStream = new MemoryStream())
                        {
                            stream.CopyTo(memoryStream);
                            imageArray = memoryStream.ToArray();
                        }
                        var file = new FileModel
                        {
                            Name = fileName.First(),
                            Extension = fileName.Last(),
                            FolderId = 1,
                            Content = Convert.ToBase64String(imageArray)
                        };                        
                        await FileRepository.AddItemAsync(file);
                    }
                }
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }
        }
    }
}