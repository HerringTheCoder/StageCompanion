using System;
using System.Diagnostics;
using System.Threading.Tasks;
using PropertyChanged;
using StageCompanion.Interfaces;
using StageCompanion.Models;
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
        
        private IFileService FileService => DependencyService.Get<IFileService>();

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            PickImage = new Command(async () => await PickAndSend());
            User = App.CurrentUser;
        }

        public async Task PickAndSend(PickOptions options = null)
        {
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    await FileService.SendFile(stream, result);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}