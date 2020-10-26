using System;
using System.Collections.Generic;
using recipe_demo.ViewModels;
using Xamarin.Forms;

namespace recipe_demo.Views
{
    public partial class RecipeEntryView : ContentPage
    {
        private RecipeEntryViewModel recipeEntryViewModel;

        public RecipeEntryView()
        {
            InitializeComponent();
        }

        public RecipeEntryView(RecipeEntryViewModel recipeEntryViewModel)
        {
            this.recipeEntryViewModel = recipeEntryViewModel;
        }
    }
}
