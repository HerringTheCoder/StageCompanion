using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StageCompanion.Models;
using StageCompanion.Views;
using StageCompanion.ViewModels;

namespace StageCompanion.Views
{
    public partial class FoldersPage : ContentPage
    {
        FoldersViewModel _foldersViewModel;

        public FoldersPage()
        {
            InitializeComponent();

            BindingContext = _foldersViewModel = new FoldersViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _foldersViewModel.OnAppearing();
        }
    }
}