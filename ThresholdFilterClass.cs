using NUnit.Framework;
using System;

namespace Recognizer
{
    [TestFixture]
    public class Class
    {
        [Test]
        public void ThresholdFilterOneXTwo() // 1x2
        {
            var original = new double[,] { { 1, 0 } };
            var expected = new double[,] { {1.0, 0.0} };
            var actual = ThresholdFilterClass.ThresholdFilter(original, 0.5);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThresholdFilterOneXTwoSecond() // 1x2
        {
            var original = new double[,] { { 1, 2 } };
            var expected = new double[,] { { 0.0, 1.0 } };
            var actual = ThresholdFilterClass.ThresholdFilter(original, 0.5);
            Assert.AreEqual(expected, actual);
        }
    }

    public static class ThresholdFilterClass
	{
        public static void FillInAllSpecifiedColors(double color, double[,] result, double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);

            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                    if (Math.Abs(original[i, j] - color) < double.Epsilon)
                        result[i, j] = 1.0;
                        
        }

        public static void ConvertMatrixToSingleArray(double[, ] matrix, double[] array)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);

            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                    array[i* height + j] = matrix[i, j];
        }

        public static double CalculateThresholdValue(double[,] original, double whitePixelsFraction) // countWhitePixels никогда не 0
        {
            var array = new double[original.Length];
            ConvertMatrixToSingleArray(original, array);

            Array.Sort(array);

            var minWhitePixels = (int)(whitePixelsFraction * original.Length);

            var thresholdValue = double.MaxValue;
            if (minWhitePixels != 0)
                thresholdValue = array[array.Length - minWhitePixels];

            return thresholdValue;
        }

        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var threshold = CalculateThresholdValue(original, whitePixelsFraction);

            var minWhitePixels = (int)(whitePixelsFraction * original.Length);
            var result = new double[width, height];
            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                {
                    if (minWhitePixels == 0)
                        break;
                    if (original[i, j] >= threshold && result[i, j] != 1.0)
                    {
                        result[i, j] = 1.0;
                        FillInAllSpecifiedColors(original[i, j], result, original); //Этот метод закрашивает белым все одинаковые цвета, если хотя бы один из них закрасился
                        minWhitePixels--;
                    }
                }

            return result;
        }
    }
}
