
using System.Reflection.PortableExecutable;

// using UtilityWFA;

namespace PictureAnalyzisWFA
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // JPGReader.ReadJPGFile("..\\..\\..\\..\\Images\\picture.jpg");

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}