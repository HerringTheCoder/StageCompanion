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
        public User User { get; set; }

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            User = App.CurrentUser;
        }
    }
}