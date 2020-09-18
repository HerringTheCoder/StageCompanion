using System.ComponentModel;
using Xamarin.Forms;
using StageCompanion.ViewModels;

namespace StageCompanion.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}