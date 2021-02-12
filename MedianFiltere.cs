using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;


namespace Recognizer
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void MedianOneXOne() //1x1
        {
            var original = new double[,] { { 3 } };
            var actual = MedianFiltere.GetMedian(original, 0, 0);
            Assert.AreEqual(3, actual);
        }

        [Test]
        public void MedianOneXTwo() // 1x2
        {
            var original = new double[,] {{0.1, 0.3}};
            var actual = MedianFiltere.GetMedian(original, 0, 0);
            Assert.AreEqual(0.2, actual);
        }

        [Test]
        public void MedianFilterOneXTwo() // 1x2
        {
            var original = new double[,] { { 0.1, 0.3 } };
            var actual = MedianFiltere.MedianFilter(original);
            var exepted = new double[, ] { { 0.2, 0.2 } };
            Assert.AreEqual(exepted, actual);

        }

        [Test]
        public void MedianFilterTreeXTwo() // 3x2
        {
            var original = new double[,] { { 1, 0 }, { 1, 1 }, { 1, 1 } };
            var actual = MedianFiltere.MedianFilter(original);
            var exepted = new double[,] { { 1, 1 }, { 1, 1 }, { 1, 1 } };
            Assert.AreEqual(exepted, actual);

        }


    }


    internal static class MedianFiltere
	{
        public static double GetMedian(double[,] original, int x, int y)
        {
            var square = new List<double>();
            var width = original.GetLength(0);
            var height = original.GetLength(1);

            for (var i = x-1; i < x + 2; i++)
            {
                if (i >= 0 && i < width)
                    for (var j = y - 1; j < y + 2; j++)
                    {
                        if (j >= 0 && j < height)
                            square.Add(original[i, j]);
                    }
            }
            square.Sort();
            var median = square[square.Count / 2];
            if (square.Count % 2 == 0)
                median = (square[square.Count / 2 - 1] + square[square.Count / 2]) / 2;

            return median;
        }

        public static double[,] MedianFilter(double[,] original)
		{
            var resultArray = new double[original.GetLength(0), original.GetLength(1)];
            for (var x = 0; x < original.GetLength(0); x++)
                for (var y = 0; y < original.GetLength(1); y++)
                    resultArray[x, y] = GetMedian(original, x, y);

            return resultArray;
		}
	}
}