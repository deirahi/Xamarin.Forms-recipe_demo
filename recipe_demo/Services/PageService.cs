using System;
using System.Collections.Generic;
using System.ComponentModel;
using recipe_demo.ViewModels;
using recipe_demo.Services;
using Xamarin.Forms;
using System.Linq;
using recipe_demo.Models;
using System.Threading.Tasks;

namespace recipe_demo.Services
{
    public class PageService : IPageService
    {
        private Page mainPage
        {
            get { return Application.Current.MainPage; }
        }


        public async Task<Page> PopAsync()
        {
            return await mainPage.Navigation.PopAsync();
        }

        public async Task PushAsync(Page page)
        {
            await mainPage.Navigation.PushAsync(page);
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cansel)
        {
            return await mainPage.DisplayAlert(title, message, ok, cansel);
        }

        public async Task DisplayAlert(string title, string message, string ok)
        {
            await mainPage.DisplayAlert(title, message, ok);
        }
    }
}
