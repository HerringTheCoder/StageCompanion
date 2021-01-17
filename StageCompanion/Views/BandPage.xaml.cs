using System.Diagnostics;
using StageCompanion.Models;
using StageCompanion.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BandPage : ContentPage
    {
        private readonly BandViewModel _bandViewModel;

        public BandPage()
        {
            InitializeComponent();
            BindingContext = _bandViewModel = new BandViewModel();
            Debug.WriteLine("Test");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _bandViewModel.LoadBand();
        }
    }
}