using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using recipe_demo.Models;
using recipe_demo.Views;
using recipe_demo.Services;
using Xamarin.Forms;
using System.Windows.Input;

namespace recipe_demo.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private RecipeDetailViewModel vmSelectedRecipe;
        private IDbService vmDbService;
        private IPageService vmPageService;

        private bool vmIsDataLoaded;

        //todo:ここのViewmodelがEntryでいいのかは調査・検証が必要
        public ObservableCollection<RecipeEntryViewModel> Recipes { get; private set; }
         = new ObservableCollection<RecipeEntryViewModel>();

        public RecipeDetailViewModel SelectedRecipe
        {
            get { return vmSelectedRecipe; }
            set { SetValue(ref vmSelectedRecipe, value); }
        }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand SelectRecipeCommand { get; private set; }
        public ICommand AddRecipeCommand { get; private set; }
        //todo: DeleteはとりあえずはMainViewからではなくDetailから行う予定　なので、コマンドだけ置いておく
        public ICommand DeleteRecipeCommand { get; private set; }

        public MainViewModel(IDbService dbService, IPageService pageService)
        {
            vmDbService = dbService;
            vmPageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            AddRecipeCommand = new Command(async () => await AddRecipe());
            SelectRecipeCommand = new Command<RecipeDetailViewModel>(async r => await SelectData(r));

            MessagingCenter.Subscribe<RecipeEntryViewModel, Recipe>
                (this, DbChangeEventMassages.RecipeAdded, OnRecipeAdded);

            MessagingCenter.Subscribe<RecipeEntryViewModel, Recipe>
                (this, DbChangeEventMassages.RecipeUpdated, OnRecipeUpdated);
            //todo: DeleteのSubscribeが必要かを検証すること
        }

        private void OnRecipeUpdated(RecipeEntryViewModel source, Recipe recipe)
        {
            var recipeConcerned = Recipes.Single(r => r.RecipeId == recipe.RecipeId);

            recipeConcerned.RecipeId = recipe.RecipeId;
            recipeConcerned.RecipeName = recipe.RecipeName;
            recipeConcerned.Explanation = recipe.Explanation;
            recipeConcerned.SetDate = recipe.SetDate;
            recipeConcerned.PhotoFilepath = recipe.PhotoFilepath;
            recipeConcerned.PhotoByte = recipe.PhotoByte;
            recipeConcerned.Items = recipe.Items;
            recipeConcerned.Steps = recipe.Steps;
        }

        private void OnRecipeAdded(RecipeEntryViewModel source, Recipe recipe)
        {
            Recipes.Add(new RecipeEntryViewModel(recipe));
        }

        private async Task AddRecipe()
        {
            await vmPageService.PushAsync(new RecipeEntryView(new RecipeEntryViewModel()));
        }

        private async Task SelectData(object recipe)
        {
            if(recipe == null)
                return;
            vmSelectedRecipe = null;
            await vmPageService.PushAsync(new RecipeDetailView(recipe));
        }

        private async Task LoadData()
        {
            if (vmIsDataLoaded)
                return;

            vmIsDataLoaded = true;
            var recipes = await vmDbService.GetRecipesAsync();
            foreach ( var recipe in recipes)
            {
                Recipes.Add(new RecipeEntryViewModel(recipe));
            }
        }
    }
}
