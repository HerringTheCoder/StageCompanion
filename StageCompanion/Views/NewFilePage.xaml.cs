using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using StageCompanion.Models;
using StageCompanion.ViewModels;

namespace StageCompanion.Views
{
    public partial class NewFilePage : ContentPage
    {
        public Item Item { get; set; }

        public NewFilePage()
        {
            InitializeComponent();
            BindingContext = new NewFileViewModel();
        }
    }
}