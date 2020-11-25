using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using recipe_demo.Models;
using System.Linq;
using recipe_demo.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using System.IO;
using System.Drawing;
using Xamarin.Forms.Shapes;

namespace recipe_demo.ViewModels
{
    public class RecipeEntryViewModel: BaseViewModel
    {
        //DB保存、画面表示用
        public Recipe recipe { get; private set; }

        ImageSource _recipePhotoSource;
        public ImageSource RecipePhotoSource
        {
            get { return _recipePhotoSource; }
            set { SetValue(ref _recipePhotoSource, value ); }
        }

        //参照渡しでデータの削除をするためのポインタのような役割
        private RecipeEntryModel ThisRecipeEntry;


        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Step> Steps { get; set; }

        private readonly IDbService dbService;
        private readonly IPageService pageService;

        public ICommand ItemAddCommand { get => new Command(() => ItemAdd()); }
        public ICommand ItemDeleteCommand { get => new Command(i => ItemDelete(i)); }
        public ICommand StepAddCommand { get => new Command(() => StepAdd());}
        public ICommand StepDeleteCommand { get => new Command(s => StepDelete(s)); }

        public ICommand SaveCommand { get => new Command(async () => await Save()); }
        public ICommand DeleteCommand { get => new Command(async () => await Delete()); }
        public ICommand PickPhotoCommand { get=> new Command(async () => await PickPhoto()); }


        public RecipeEntryViewModel(Recipe recipe)
        {
            this.recipe = recipe;
        }

        public RecipeEntryViewModel(RecipeEntryModel recipeEntryModel, DBService dbService, PageService pageservice)
        {
            if (recipeEntryModel == null)
            {
                throw new ArgumentNullException(nameof(recipeEntryModel));
            }

            ThisRecipeEntry = recipeEntryModel;
            this.dbService = dbService;
           this.pageService = pageservice;

            recipe = new Recipe
            {
                RecipeId = recipeEntryModel.EntryRecipeId,
                RecipeName = recipeEntryModel.RecipeName,
                Explanation = recipeEntryModel.Explanation,
                PhotoBytes = recipeEntryModel.PhotoBytes,
                Items = recipeEntryModel.Items,
                Steps = recipeEntryModel.Steps
            };

            Items = recipeEntryModel.Items != null ? new ObservableCollection<Item>(recipe.Items) : new ObservableCollection<Item>();
            Steps = recipeEntryModel.Steps != null ? new ObservableCollection<Step>(recipe.Steps) : new ObservableCollection<Step>();

            RecipePhotoSource = recipeEntryModel.PhotoFileSource ;
        }

        //画面の材料欄の増減
        public void ItemAdd()
        {

            Items.Add( new Item());
        }

        public void ItemDelete(object sender) 
        {
            var item = (Item)sender;
            Items.Remove(item);
        }

        //画面の手順欄の増減
        public void StepAdd()
        {
            Steps.Add(new Step());
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
                if (await pageService.DisplayAlert("警告", $"新しいレシピを登録せずに破棄しますが、よろしいですか?", "はい", "いいえ"))
                {
                    await pageService.PopAsync();
                }
                return;
            }

            if (await pageService.DisplayAlert("警告", $"本当に「{recipe.RecipeName}」を削除しますか?", "はい", "いいえ"))
            {

                await dbService.DeleteRecipe(recipe);
                MessagingCenter.Send(this, DbChangeEventMassages.RecipeDeleted, ThisRecipeEntry);

                await pageService.PopAsync();
            }

        }

        //画像を取得しリサイズして表示・ローカル変数に格納
        private async Task PickPhoto()
        {

            Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
            if (stream != null)
            {
                recipe.PhotoBytes = ImageConversion.GetImageBytes(stream);
                //上限サイズを超えていたら、上限を下回る大きさに縮小する
                if(recipe.PhotoBytes.Length > ImageConversion.UpperLimitImageBytes)
                {
                    //500kbとなるような倍率を計算 TODO:必ずしも500kb以下にならないのでリサイズ方法を検討すること
                    float scale = (float) ImageConversion.UpperLimitImageBytes / (float) recipe.PhotoBytes.Length;

                    // サイズ変更した画像を作成する
                    var resizeImageBytes = DependencyService.Get<IImageResize>().ResizeImage(recipe.PhotoBytes, scale, scale);
                    recipe.PhotoBytes = resizeImageBytes;
                }

                //画面表示用にストリームをつくる　こうしないと画面に画像が表示されなかった
                RecipePhotoSource = ImageSource.FromStream(() => ImageConversion.BytesToStream(recipe.PhotoBytes));

            }

        }



        async Task Save()
        {
            if (String.IsNullOrEmpty(recipe.RecipeName) )
            {
                await pageService.DisplayAlert("Error", "レシピ名を入力してください。", "OK");
                return;
            }

            recipe.Items = new List<Item>(Items);
            recipe.Steps = new List<Step>(Steps);


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
