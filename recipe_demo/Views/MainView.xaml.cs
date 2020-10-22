using System;
using System.Collections.Generic;
using System.ComponentModel;
using recipe_demo.ViewModels;
using recipe_demo.Services;
using Xamarin.Forms;
using System.Linq;
using recipe_demo.Models;

namespace recipe_demo.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            var recipesDB = new DBService(DependencyService.Get<IDbConnection>());
            var pageService = new PageService();

            ViewModel = new MainViewModel(recipesDB, pageService);

            InitializeComponent();
            
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }
        // todo: コマンドをビューモデルとバインディングする　メモとコンタクトのDeleteを参考
        //void OnRecipeSelected(object sender, SelectionChangedEventArgs e)
        //{
        //    ViewModel.SelectRecipeCommand.Execute(e.CurrentSelection.FirstOrDefault() as Recipe );
        //}

        public MainViewModel ViewModel
        {
            get { return BindingContext as MainViewModel; }
            set { BindingContext = value; }
        }
    }
}
