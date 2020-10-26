using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace recipe_demo.Views
{
    public partial class RecipeDetailView : ContentPage
    {
        private object recipe;

        public RecipeDetailView()
        {
            InitializeComponent();
        }

        public RecipeDetailView(object recipe)
        {
            this.recipe = recipe;
        }
    }
}
