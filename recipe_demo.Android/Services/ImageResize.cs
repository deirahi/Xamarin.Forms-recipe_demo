using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using recipe_demo.Droid.Services;
using recipe_demo.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageResize))]
namespace recipe_demo.Droid.Services
{
    public class ImageResize : IImageResize
    {
        public ImageResize() { }

        public byte[] ResizeImage(byte[] imageData, float widthScale, float heightScale)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            var width = (int)(originalImage.Width * widthScale);
            var height = (int)(originalImage.Height * heightScale);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage,width , height , false);

            using (MemoryStream ms = new MemoryStream())
            {
                // pngとしたときのバイト配列を返す
                resizedImage.Compress(Bitmap.CompressFormat.Png, 100, ms);
                return ms.ToArray();
            }
        }
    }
}
