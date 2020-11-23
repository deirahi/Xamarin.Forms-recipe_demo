using System;
using System.Collections.Generic;
using recipe_demo.Services;
using recipe_demo.ViewModels;
using Xamarin.Forms;

namespace recipe_demo.Views
{
    public partial class RecipeDetailView : ContentPage
    {
        public RecipeDetailView(RecipeEntryModel recipeEntry)
        {
            InitializeComponent();
            var pageservice = new PageService();
            BindingContext = new RecipeDetailViewModel(recipeEntry, pageservice);
        }
    }
}
