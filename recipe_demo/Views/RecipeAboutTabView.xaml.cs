using System;
using System.Collections.Generic;
using Xamarin.Forms;
using recipe_demo.ViewModels;
using recipe_demo.Models;

namespace recipe_demo.Views
{
    public partial class RecipeAboutTabView:ContentView

    {
        public RecipeAboutTabView(RecipeEntryModel recipeEntry)
        {
            InitializeComponent();
            recipeTitle.Text = recipeEntry.RecipeName;
            recipeImage.Source = recipeEntry.PhotoFileSource;
            recipeExplanation.Text = recipeEntry.Explanation;
        }
    }
}
