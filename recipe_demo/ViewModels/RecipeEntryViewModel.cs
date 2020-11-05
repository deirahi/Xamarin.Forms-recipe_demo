using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using recipe_demo.Models;
using System.Linq;
using recipe_demo.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;

namespace recipe_demo.ViewModels
{
    public class RecipeEntryViewModel: BaseViewModel
    {
        public Recipe recipe { get; private set; }

        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Step> Steps { get; set; }

        private readonly IDbService dbService;
        private readonly IPageService pageService;

        public ICommand ItemAddCommand { get; private set; }
        public ICommand ItemDeleteCommand { get; private set; }
        public ICommand StepAddCommand { get; private set; }
        public ICommand StepDeleteCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }


        public RecipeEntryViewModel(Recipe recipe)
        {
            this.recipe = recipe;
        }

        public RecipeEntryViewModel(RecipeEntryModel recipeEntryModel, DBService dbService, PageService pageservice)
        {
            if (recipeEntryModel == null)
                throw new ArgumentNullException(nameof(recipeEntryModel));

            this.dbService = dbService;
           this.pageService = pageservice;

            recipe = new Recipe
            {
                RecipeId = recipeEntryModel.EntryRecipeId,
                RecipeName = recipeEntryModel.RecipeName,
                Explanation = recipeEntryModel.Explanation,
                PhotoFilePath = recipeEntryModel.PhotoFilepath,
                PhotoByte = recipeEntryModel.PhotoByte,
                Items = recipeEntryModel.Items,
                Steps = recipeEntryModel.Steps
            };

            Items = recipeEntryModel.Items != null ? new ObservableCollection<Item>(recipe.Items) : new ObservableCollection<Item>();
            Steps = recipeEntryModel.Items != null ? new ObservableCollection<Step>(recipe.Steps) : new ObservableCollection<Step>();

            if (Items.Count == 0)
            {
                Items.Add(new Item());
            }
            if (Steps.Count == 0)
            {
            }
            ItemAddCommand = new Command(() => ItemAdd());
            ItemDeleteCommand = new Command(i => ItemDelete(i));

            StepAddCommand = new Command(() => StepAdd());
            StepDeleteCommand = new Command( s => StepDelete(s));

            SaveCommand = new Command(async () => await Save());
            DeleteCommand = new Command(async () => await Delete());

        }

        public void ItemAdd()
        {

            Items.Add( new Item());
        }

        public void ItemDelete(object sender) 
        {
            var item = (Item)sender;
            Items.Remove(item);
        }

        public void StepAdd()
        {
            Steps.Add(new Step() { StepOrder = Steps.Count});
        }

        public void StepDelete( object sender)
        {
            var step = (Step)sender;

            Steps.Remove(step);
        }

        private async Task Delete()
        {
            if (recipe.RecipeId == 0)
            {
                if (await pageService.DisplayAlert("Warning", $"Are you sure you want to throw away this new recipe ?", "Yes", "No"))
                {
                    await pageService.PopAsync();
                }
                return;
            }

            if (await pageService.DisplayAlert("Warning", $"Are you sure you want to delete {recipe.RecipeName}?", "Yes", "No"))
            {

                await dbService.DeleteRecipe(recipe);
                MessagingCenter.Send(this, DbChangeEventMassages.RecipeDeleted, recipe);

                await pageService.PopAsync();
            }

        }


        async Task Save()
        {
            if (String.IsNullOrEmpty(recipe.RecipeName) )
            {
                await pageService.DisplayAlert("Error", "Please enter the name.", "OK");
                return;
            }

            recipe.Steps = new List<Step>(Steps);
            recipe.Items = new List<Item>(Items);

            if (recipe.RecipeId == 0)
            {
                await dbService.AddRecipe(recipe);
                MessagingCenter.Send(this, DbChangeEventMassages.RecipeAdded, recipe);
            }
            else
            {
                await dbService.UpdateRecipe(recipe);
                MessagingCenter.Send(this, DbChangeEventMassages.RecipeUpdated, recipe);
            }
            await pageService.PopAsync();
        }

    }
}
