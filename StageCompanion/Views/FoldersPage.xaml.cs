using Xamarin.Forms;
using StageCompanion.ViewModels;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace StageCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoldersPage : ContentPage
    {
        private readonly FoldersViewModel _foldersViewModel;

        public FoldersPage()
        {     
            InitializeComponent();
            _foldersViewModel = new FoldersViewModel();
            BindingContext = _foldersViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _foldersViewModel.OnAppearing();
        }       
    }
}