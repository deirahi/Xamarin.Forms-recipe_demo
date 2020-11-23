
using System;
using System.IO;
using System.Threading.Tasks;

namespace recipe_demo.Services
{
    //参考；https://github.com/xamarin/xamarin-forms-samples/blob/master/XamFormsImageResize/XamFormsImageResize/ImageResizer.cs
    public interface IImageResize
    {
        byte[] ResizeImage(byte[] imageData, float widthScale, float heightScale);
    }
}
