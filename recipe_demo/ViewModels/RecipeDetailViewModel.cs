using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using recipe_demo.Models;
using recipe_demo.Services;
using recipe_demo.Views;
using Xamarin.Forms;

namespace recipe_demo.ViewModels
{
    public class RecipeDetailViewModel:BaseViewModel
    {

        private readonly IPageService iPageService;

        public RecipeEntryModel recipeEntry { get; private set; }

        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Step> Steps { get; set; }


        private int _positionSelected = 0;
        public int PositionSelected
        {
            set
            {
                if (_positionSelected != value)
                {
                    _positionSelected = value;

                    OnPropertyChanged(nameof(PositionSelected));
                }
            }
            get => _positionSelected;
        }

        private List<ContentView> _contentViews;
        public List<ContentView> ContentViews
        {
            get { return _contentViews; }
            set { SetValue(ref _contentViews, value); }
        }

        public ICommand RecipeEditCommand { get => new Command(async () => await RecipeEdit()); }

        public ICommand SelectTabCommand { get => new Command<string>((param) => PositionSelected = int.Parse(param)); }

        //コンストラクタ
        public RecipeDetailViewModel(RecipeEntryModel entry , PageService pageService)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            iPageService = pageService;

            recipeEntry = entry;

            Items = recipeEntry.Items != null ? new ObservableCollection<Item>(recipeEntry.Items) : new ObservableCollection<Item>();
            Steps = recipeEntry.Steps != null ? new ObservableCollection<Step>(recipeEntry.Steps) : new ObservableCollection<Step>();



            //カルーセルビュー用の画面を用意
            ContentViews = new List<ContentView>
            {
                new RecipeAboutTabView(recipeEntry),
                new RecipeItemsTabView(recipeEntry.Items),
                new RecipeStepsTabView(recipeEntry.Steps)
            };
        }

        private async Task RecipeEdit()
        {
            await iPageService.PopAsync();
            _ = iPageService.PushAsync(new RecipeEntryView(this.recipeEntry));
        }
    }
}
