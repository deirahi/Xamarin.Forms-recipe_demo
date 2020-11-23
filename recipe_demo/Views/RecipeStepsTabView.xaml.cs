using System;
using System.Collections.Generic;
using Xamarin.Forms;
using recipe_demo.ViewModels;
using recipe_demo.Models;

namespace recipe_demo.Views
{
    public partial class RecipeStepsTabView:ContentView
    {
        public RecipeStepsTabView()
        {
            InitializeComponent();
        }
        public RecipeStepsTabView(List<Step> steps)
        {
            InitializeComponent();
            StepsStack.ItemsSource = steps;
        }
    }
}
