using System;
using System.IO;

namespace recipe_demo.Services
{
    public static class ImageConversion
    {
        //画像のDB保存時の上限サイズ（バイト） TODO:この数値でいいのか、レシピを多数登録して確認・検討する
        public const int UPPER_LIMIT_IMAGE_BYTES = 512000;

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
