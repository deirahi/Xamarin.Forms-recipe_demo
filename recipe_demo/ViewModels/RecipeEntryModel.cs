using System;
using System.Collections.Generic;
using recipe_demo.Models;
using recipe_demo.Services;
using Xamarin.Forms;

namespace recipe_demo.ViewModels
{
    public class RecipeEntryModel:BaseViewModel
    {

        public int EntryRecipeId { get; set; }

        public RecipeEntryModel() { }

        public RecipeEntryModel(Recipe recipe)
        {
            EntryRecipeId = recipe.RecipeId;
            EntryRecipeName = recipe.RecipeName;
            EntryExplanation = recipe.Explanation;
            EntryPhotoBytes = recipe.PhotoBytes;
            EntryRecipeItems = recipe.Items;
            EntryRecipeSteps = recipe.Steps;

            if (recipe.PhotoBytes != null)
            {
                EntryPhotoFileSource = ImageSource.FromStream(() => ImageConversion.BytesToStream(recipe.PhotoBytes));
            }
        }

        private string EntryRecipeName;
        public string RecipeName
        {
            get { return EntryRecipeName; }
            set
            {
                SetValue(ref EntryRecipeName, value, nameof(RecipeName));
            }
        }

        private string EntryExplanation;
        public string Explanation
        {
            get { return EntryExplanation; }
            set
            {
                SetValue(ref EntryExplanation, value,nameof(Explanation));
            }
        }

        private byte[] EntryPhotoBytes;
        public byte[] PhotoBytes
        {
            get { return EntryPhotoBytes; }
            set
            {
                SetValue(ref EntryPhotoBytes, value, nameof(PhotoBytes));
            }
        }

        //画像の画面表示用
        private ImageSource EntryPhotoFileSource;
        public ImageSource PhotoFileSource
        {
            get { return EntryPhotoFileSource; }
            set
            {
                EntryPhotoFileSource = ImageSource.FromStream(() => ImageConversion.BytesToStream(EntryPhotoBytes));
                OnPropertyChanged(nameof(PhotoFileSource));

            }
        }

        private List<Item> EntryRecipeItems;
        public List<Item> Items
        {
            get { return EntryRecipeItems; }
            set
            {
                SetValue(ref EntryRecipeItems, value,nameof(Items));
            }
        }

        private List<Step> EntryRecipeSteps;
        public List<Step> Steps
        {
            get { return EntryRecipeSteps; }
            set
            {
                SetValue(ref EntryRecipeSteps, value, nameof(Items));
            }
        }

    }
}
