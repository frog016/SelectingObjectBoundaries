using System;
using System.Diagnostics;
using System.Drawing;
using NUnit.Framework;

namespace Recognizer
{
    [TestFixture]
    public class MyClass
    {
        public Pixel[,] GenerateImage()
        {
            var bmp = (Bitmap)Image.FromFile("eurobot.bmp");
            return Program.LoadPixels(bmp);
        }

        [Test]
        public void GetLengthInLoop()
        {
            var pixels = GenerateImage();

            Stopwatch time = new Stopwatch();
            time.Start();
            var picture = Grayscale.ToGrayscaleGetInLoop(pixels);
            time.Stop();
            Console.Out.WriteLine(time.ElapsedTicks);
        }

        [Test]
        public void GetLengthBeforLoop()
        {
            var pixels = GenerateImage();

            Stopwatch time = new Stopwatch();
            time.Start();
            var picture = Grayscale.ToGrayscale(pixels);
            time.Stop();
            Console.Out.WriteLine(time.ElapsedTicks);
        }

        [Test]
        public void CalcInLoop()
        {
            var pixels = GenerateImage();

            Stopwatch time = new Stopwatch();
            time.Start();
            var picture = Grayscale.ToGrayscaleCalculateColorInLoop(pixels);
            time.Stop();
            Console.Out.WriteLine(time.ElapsedTicks);
        }
    }

    public static class Grayscale
	{
        public static double ConvertToGray(int red, int green, int blue)
        {
            return (0.299 * red + 0.587 * green + 0.114 * blue) / 255;

        }

        public static double[,] ToGrayscale(Pixel[,] original) //вызов до цикла метода GetLength
        {
            var widthPicture = original.GetLength(0);
            var heightPicture = original.GetLength(1);
            var result = new double[original.GetLength(0), original.GetLength(1)];
			
            for (var x = 0; x < widthPicture; x++)
            for (var y = 0; y < heightPicture; y++)
                result[x, y] = ConvertToGray(original[x, y].R, original[x, y].G, original[x, y].B);

            return result;
		}

        public static double[,] ToGrayscaleGetInLoop(Pixel[,] original) // вызов в условии цикла метода GetLength
        {
            var result = new double[original.GetLength(0), original.GetLength(1)];

            for (var x = 0; x < original.GetLength(0); x++)
                for (var y = 0; y < original.GetLength(1); y++)
                    result[x, y] = ConvertToGray(original[x, y].R, original[x, y].G, original[x, y].B);

            return result;
        }

        public static double[,] ToGrayscaleCalculateColorInLoop(Pixel[,] original) //вызов до цикла метода GetLength
        {
            var widthPicture = original.GetLength(0);
            var heightPicture = original.GetLength(1);
            var result = new double[original.GetLength(0), original.GetLength(1)];

            for (var x = 0; x < widthPicture; x++)
            for (var y = 0; y < heightPicture; y++)
                result[x, y] = (0.299 * original[x, y].R + 0.587 * original[x, y].G + 0.114 * original[x, y].B) / 255;

            return result;
        }
    }
}