using System.ComponentModel;
using Xamarin.Forms;
using StageCompanion.ViewModels;

namespace StageCompanion.Views
{
    public partial class FileDetailPage : ContentPage
    {
        private FileDetailViewModel _fileDetailViewModel;

        public FileDetailPage()
        {
            InitializeComponent();
            BindingContext = _fileDetailViewModel = new FileDetailViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _fileDetailViewModel.LoadFile();
        }
    }
}