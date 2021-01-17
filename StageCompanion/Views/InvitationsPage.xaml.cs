using StageCompanion.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InvitationsPage : ContentPage
    {
        private readonly InvitationsViewModel _invitationsViewModel;

        public InvitationsPage()
        {
            InitializeComponent();
            _invitationsViewModel = new InvitationsViewModel();
            BindingContext = _invitationsViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _invitationsViewModel.OnAppearing();
        }
    }
}