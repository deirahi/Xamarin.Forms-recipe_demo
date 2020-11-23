using System;
using System.IO;
using System.Threading.Tasks;

namespace recipe_demo.Services
{
    //引用：https://docs.microsoft.com/ja-jp/xamarin/xamarin-forms/app-fundamentals/dependency-service/photo-picker
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
