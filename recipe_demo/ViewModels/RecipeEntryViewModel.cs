using System;
using System.Collections.Generic;
using recipe_demo.Models;

namespace recipe_demo.ViewModels
{
    public class RecipeEntryViewModel: BaseViewModel
    {
        private Recipe recipe;

        public RecipeEntryViewModel()
        {
        }

        public RecipeEntryViewModel(Recipe recipe)
        {
            this.recipe = recipe;
        }

        public int RecipeId { get; internal set; }
        public string RecipeName { get; internal set; }
        public string SetDate { get; internal set; }
        public string PhotoFilepath { get; internal set; }
        public byte PhotoByte { get; internal set; }
        public string Explanation { get; internal set; }
        public List<Item> Items { get; internal set; }
        public List<Step> Steps { get; internal set; }
    }
}
