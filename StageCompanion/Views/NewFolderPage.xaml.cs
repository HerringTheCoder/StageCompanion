using StageCompanion.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StageCompanion.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewFolderPage : ContentPage
    {
        public NewFolderPage()
        {
            InitializeComponent();
            BindingContext = new NewFolderViewModel();
        }
    }
}