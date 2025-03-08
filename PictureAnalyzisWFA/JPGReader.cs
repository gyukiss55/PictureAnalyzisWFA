using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics; // Required for Debug
using System.Runtime.InteropServices;



namespace PictureAnalyzisWFA
{
    public class JPGReader
    {
        Bitmap? bitmap;

        public JPGReader()
        {
            bitmap = null;
        }

        public static void ReadJPGFile (string imagePath)
        {
            using (Image img = Image.FromFile(imagePath))
            {
                int width = img.Width;
                int height = img.Height;

                // Console.WriteLine($"Width: {width}, Height: {height}");
                Debug.WriteLine($"Width: {width}, Height: {height}");
                


                using (Bitmap bitmapLocal = new Bitmap(img))
                {
                    for (int y = height/2; y < height*3/4; y+=10)
                    {
                        for (int x = width/2; x < width*3/4; x+=10)
                        {
                            Color pixelColor = bitmapLocal.GetPixel(x, y);
                            // Console.WriteLine($"Pixel at ({x}, {y}) is {pixelColor}");
                            // Debug.WriteLine($"Pixel at ({x}, {y}) is {pixelColor}");
                        }
                    }
                }
            }

        }
        public bool ReadJPGFile(string imagePath, out Bitmap bitmapOut, Size size)
        {
            using (Image img = Image.FromFile(imagePath))
            {
                int width = img.Width;
                int height = img.Height;

                // Console.WriteLine($"Width: {width}, Height: {height}");
                Debug.WriteLine($"Width: {width}, Height: {height}");

                using (bitmap = new Bitmap(img, size))
                {
                    Debug.WriteLine($" New Width: {size.Width}, Height: {size.Height}");
                    for (int y = size.Height / 2; y < size.Height * 3 / 4; y += 10)
                    {
                        for (int x = size.Width / 2; x < size.Width * 3 / 4; x += 10)
                        {
                            Color pixelColor = bitmap.GetPixel(x, y);
                            // Console.WriteLine($"Pixel at ({x}, {y}) is {pixelColor}");
                            Debug.WriteLine($"Pixel at ({x}, {y}) is {pixelColor}");
                        }
                    }
                    bitmapOut = new Bitmap(bitmap);
                    return true;
                }
            }

        }
        public static void SetPixelFast(Bitmap bmp, int x1, int y1, int x2, int y2, Color color)
        {
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                                              ImageLockMode.ReadWrite, bmp.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            IntPtr ptr = bmpData.Scan0;
            int stride = bmpData.Stride;
            byte[] pixelData = new byte[Math.Abs(stride) * bmp.Height];
            for (int y = y1; y < y2; y ++)
            {
                for (int x = x1; x < x2; x ++)
                {
                    int index = (y * stride) + (x * bytesPerPixel);

                    Marshal.Copy(ptr, pixelData, 0, pixelData.Length);

                    // Set pixel color (assuming 32bpp)
                    pixelData[index] = color.B;     // Blue
                    pixelData[index + 1] = color.G; // Green
                    pixelData[index + 2] = color.R; // Red
                    if (bytesPerPixel == 4)
                        pixelData[index + 3] = color.A; // Alpha

                    Marshal.Copy(pixelData, 0, ptr, pixelData.Length);

                }

            }

            bmp.UnlockBits(bmpData);
        }
    }
}