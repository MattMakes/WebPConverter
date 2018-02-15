using System;
using System.Drawing;
using System.IO;
using WebPConsole.ext;

namespace WebPConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = GetPathFromUser();

            if (Directory.Exists(path))
            {
                ProcessImageFiles(path);
            }
            else
            {
                Console.WriteLine("No path was found with that name, please try again.");
                Console.ReadLine();
            }
        }

        private static string GetPathFromUser()
        {
            Console.WriteLine("Enter the path that you would like to convert:");
            var path = Console.ReadLine();

            return path;
        }

        private static void ProcessImageFiles(string path)
        {
            var jpgFiles = Directory.GetFiles(path, "*.jpg");
            var pngFiles = Directory.GetFiles(path, "*.png");

            Console.WriteLine($"Reading {jpgFiles.Length} JPG files and {pngFiles.Length} PNG files in this location");
            Console.WriteLine("Proceed?");
            Console.ReadLine();

            ConvertImagesToWebP(jpgFiles);
            ConvertImagesToWebP(pngFiles);

            Console.WriteLine("\nAll finished! Press any key to quit.");
            Console.ReadLine();
        }

        private static void ConvertImagesToWebP(string[] imagePaths)
        {
            LoadLibrary.LoadWebPOrFail();

            foreach (var imagePath in imagePaths)
            {
                Console.WriteLine($"Processing.... {imagePath}");
                var imageEncoder = new SimpleEncoder();
                var fileName = imagePath;
                var outFileName = imagePath.Replace(imagePath.Substring(imagePath.Length - 4), ".webp");

                using (var bitmapStream = File.Open(fileName, FileMode.Open))
                {
                    using(var outStream = new FileStream(outFileName, FileMode.Create))
                    {
                        var img = Image.FromStream(bitmapStream);
                        var mBitmap = new Bitmap(img);

                        imageEncoder.Encode(mBitmap, outStream, 100);
                        Console.WriteLine($"Writing new WebP image here: {outFileName}");
                    }
                }
            }
        }
    }
}