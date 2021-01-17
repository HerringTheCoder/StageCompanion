using StageCompanion.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilesPage : ContentPage
    {
        private readonly FilesViewModel _filesViewModel;

        public FilesPage()
        {
            InitializeComponent();
            BindingContext = _filesViewModel = new FilesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _filesViewModel.OnAppearing();
        }
    }
}