using System.ComponentModel;
using Xamarin.Forms;
using StageCompanion.ViewModels;

namespace StageCompanion.Views
{
    public partial class FileDetailPage : ContentPage
    {
        private readonly FileDetailViewModel _fileDetailViewModel;

        public FileDetailPage()
        {
            InitializeComponent();
            BindingContext = _fileDetailViewModel = new FileDetailViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _fileDetailViewModel.LoadFile();
        }
    }
}