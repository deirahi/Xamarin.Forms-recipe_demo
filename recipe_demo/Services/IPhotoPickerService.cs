using System;
using System.IO;
using System.Threading.Tasks;

namespace recipe_demo.Services
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
