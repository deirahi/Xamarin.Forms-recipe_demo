using System;
using System.Collections.Generic;
using recipe_demo.Services;
using recipe_demo.ViewModels;
using recipe_demo.Models;
using Xamarin.Forms;

namespace recipe_demo.Views
{
    public partial class RecipeEntryView : ContentPage
    {


        public RecipeEntryView(RecipeEntryModel recipeEntryModel)
        {
            InitializeComponent();
            var dbService = new DBService(DependencyService.Get<IDbConnection>());
            var pageservice = new PageService();
            BindingContext = new RecipeEntryViewModel(recipeEntryModel ?? new RecipeEntryModel(), dbService, pageservice); 
        }
    }
}
