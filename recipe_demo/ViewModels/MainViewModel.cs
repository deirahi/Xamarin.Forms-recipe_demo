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
    public class MainViewModel : BaseViewModel
    {
        private IDbService iDbService;
        private IPageService iPageService;

        private bool isDataLoaded;

        public ObservableCollection<RecipeEntryModel> Recipes { get; private set; }


        private RecipeEntryModel selectedRecipe;
        public RecipeEntryModel SelectedRecipe
        {
            get { return selectedRecipe; }
            set { SetValue(ref selectedRecipe, value, nameof(SelectedRecipe)); }
        }

        public ICommand LoadDataCommand { get => new Command(async () => await LoadData()); }
        public ICommand SelectRecipeCommand { get => new Command<RecipeEntryModel>(async r => await SelectData(r)); }
        public ICommand AddRecipeCommand { get => new Command(async () => await AddRecipe()); }

        //コンストラクタ
        public MainViewModel(IDbService dbService, IPageService pageService)
        {
            Recipes = new ObservableCollection<RecipeEntryModel>();
            iDbService = dbService;
            iPageService = pageService;
            SelectedRecipe = null;

            //編集画面での変更を取得し、画面に反映する処理を呼ぶ
            MessagingCenter.Subscribe<RecipeEntryViewModel, Recipe>
                (this, DbChangeEventMassages.RecipeAdded, OnRecipeAdded);

            MessagingCenter.Subscribe<RecipeEntryViewModel, Recipe>
                (this, DbChangeEventMassages.RecipeUpdated, OnRecipeUpdated);

            MessagingCenter.Subscribe<RecipeEntryViewModel, RecipeEntryModel>
                (this, DbChangeEventMassages.RecipeDeleted, OnRecipeRemoved);
        }

        private void OnRecipeUpdated(RecipeEntryViewModel source, Recipe recipe)
        {

            var recipeUpdated = Recipes.Single(r => r.EntryRecipeId == recipe.RecipeId);

            recipeUpdated.EntryRecipeId = recipe.RecipeId;
            recipeUpdated.RecipeName = recipe.RecipeName;
            recipeUpdated.Explanation = recipe.Explanation;
            recipeUpdated.PhotoBytes = recipe.PhotoBytes;
            recipeUpdated.PhotoFileSource = ImageSource.FromStream(() => ImageConversion.BytesToStream(recipe.PhotoBytes)); ;
            recipeUpdated.Items = recipe.Items;
            recipeUpdated.Steps = recipe.Steps;

        }

        private void OnRecipeAdded(RecipeEntryViewModel source, Recipe recipe)
        {
            Recipes.Add(new RecipeEntryModel(recipe));
        }

        private void OnRecipeRemoved(RecipeEntryViewModel source, RecipeEntryModel recipeEntry)
        {

            Recipes.Remove(recipeEntry);
        }

        private async Task AddRecipe()
        {
            var newRecipe = new Recipe();
            await iPageService.PushAsync(new RecipeEntryView(new RecipeEntryModel(newRecipe)));
        }

        private async Task SelectData(RecipeEntryModel selectedRecipeEntry)
        {
            if (selectedRecipeEntry == null)
            {
                return;
            }
            SelectedRecipe = null;

            await iPageService.PushAsync(new RecipeDetailView(selectedRecipeEntry));
        }

        private async Task LoadData()
        {
            if (isDataLoaded)
            {
                return;
            }

            isDataLoaded = true;
            var recipes = await iDbService.GetRecipesAsync();
            foreach (var recipe in recipes)
            {
                Recipes.Add(new RecipeEntryModel(recipe));
            }
        }
    }
}
