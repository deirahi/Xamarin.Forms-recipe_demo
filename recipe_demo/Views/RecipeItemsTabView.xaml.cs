using System;
using System.Collections.Generic;
using Xamarin.Forms;
using recipe_demo.ViewModels;
using recipe_demo.Models;

namespace recipe_demo.Views
{
    public partial class RecipeItemsTabView :ContentView
    {
        public RecipeItemsTabView()
        {
            InitializeComponent();
        }

        public RecipeItemsTabView(List<Item> items)
        {
            InitializeComponent();
            ItemsStack.ItemsSource = items;
        }
    }
}
