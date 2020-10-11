using System.Threading.Tasks;
using Xamarin.Forms;

namespace recipe_demo.Services
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task<Page> PopAsync();

        Task<bool> DisplayAlert(string title, string message, string ok, string cansel);
        Task DisplayAlert(string title, string message, string ok);
    }
}
