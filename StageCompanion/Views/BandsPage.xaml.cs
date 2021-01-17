using StageCompanion.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BandsPage : ContentPage
    {
        private readonly BandsViewModel _bandsViewModel;

        public BandsPage()
        {
            InitializeComponent();
            BindingContext = _bandsViewModel = new BandsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _bandsViewModel.OnAppearing();
        }
    }
}