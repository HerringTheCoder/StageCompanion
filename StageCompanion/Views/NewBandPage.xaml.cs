using StageCompanion.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewBandPage : ContentPage
    {
        public NewBandPage()
        {
            InitializeComponent();
            BindingContext = new NewBandViewModel();
        }
    }
}