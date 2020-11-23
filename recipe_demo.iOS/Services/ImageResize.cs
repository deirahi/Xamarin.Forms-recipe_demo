using System;
using System.IO;
using System.Threading.Tasks;
using System.Drawing;
using UIKit;
using CoreGraphics;
using recipe_demo.Services;
using Xamarin.Forms;
using recipe_demo.iOS.Services;

[assembly: Dependency(typeof(ImageResize))]
namespace recipe_demo.iOS.Services
{
    public class ImageResize:IImageResize
    {
        public ImageResize()
        {
        }

        public byte[] ResizeImage(byte[] imageData, float widthScale, float heightScale)
        {
            UIImage originalImage = ImageFromByteArray(imageData);
            UIImageOrientation orientation = originalImage.Orientation;

            var width = (int)(originalImage.Size.Width * widthScale);
            var height = (int)(originalImage.Size.Height * heightScale);
            //create a 24bit RGB image
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 (int)width, (int)height, 8,
                                                 4 * (int)width, CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst))
            {

                RectangleF imageRect = new RectangleF(0, 0, width, height);

                // draw the image
                context.DrawImage(imageRect, originalImage.CGImage);

                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);

                // pngとしたときのバイト配列を返す
                return resizedImage.AsPNG().ToArray();
            }
        }

        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }

            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                Console.WriteLine("Image load failed: " + e.Message);
                return null;
            }
            return image;
        }
    }
}
