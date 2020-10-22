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

        public ICommand LoadDataCommand { get; private set; }
        public ICommand SelectRecipeCommand { get; private set; }

        public MainViewModel(IDbService dbService, IPageService pageService)
        {
            vmDbService = dbService;
            vmPageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            SelectRecipeCommand = new Command(async r => await SelectData(r));
        }

        private Task SelectData(object r)
        {
            throw new NotImplementedException();
        }

        private Task LoadData()
        {
            throw new NotImplementedException();
        }
    }
}
