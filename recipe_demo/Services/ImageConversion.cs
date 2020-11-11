using System;
using System.IO;

namespace recipe_demo.Services
{
    public static class ImageConversion
    {
        public static byte[] GetImageBytes(Stream stream)
        {
            byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
        }

        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}
