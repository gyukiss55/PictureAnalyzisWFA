using System;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace PictureUtility
{
    public class JPGReader
    {
        public JPGReader()
        {
            //JPGReader.ReadJPGFile("picture.jpg");
        }

        public static void ReadJPGFile (string imagePath)
        {
            using (Image img = Image.FromFile(imagePath))
            {
                int width = img.Width;
                int height = img.Height;cvb

                Console.WriteLine($"Width: {width}, Height: {height}");

                using (Bitmap bitmap = new Bitmap(img))
                {
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            Color pixelColor = bitmap.GetPixel(x, y);
                            Console.WriteLine($"Pixel at ({x}, {y}) is {pixelColor}");
                        }
                    }
                }
            }

        }
    }
}