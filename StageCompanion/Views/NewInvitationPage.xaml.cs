using StageCompanion.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewInvitationPage : ContentPage
    {
        public NewInvitationPage()
        {
            InitializeComponent();
            BindingContext = new NewInvitationViewModel();
        }
    }
}