using NUnit.Framework;
using System;

namespace Recognizer
{
    internal static class SobelFilterClass
    {
        public static void TransposeMatrix(double[,] matrix, double[,] transposedMatrix)
        {
            var width = matrix.GetLength(0);
            var height = matrix.GetLength(1);

            for (var i = 0; i < height; i++)
                for (var j = 0; j < width; j++)
                    transposedMatrix[i, j] = matrix[j, i];
        }

        public static double Convolve(double[,] convolutionMatrix, double[,] imageMatrix, int positionX, int positionY)
        {
            var width = convolutionMatrix.GetLength(0);
            var height = convolutionMatrix.GetLength(1);

            var radiusConvolution = convolutionMatrix.GetLength(0) / 2;
            var pixelValue = 0.0;

            for (var i = 0; i < width; i++)
                for (var j = 0; j < height; j++)
                {
                    var x = i + positionX - radiusConvolution;
                    var y = j + positionY - radiusConvolution;
                    pixelValue += convolutionMatrix[i, j] * imageMatrix[x, y];
                }
            return pixelValue;
        }

        public static double[,] SobelFilter(double[,] imageMatrix, double[,] convolutionMatrix)
        {
            var width = imageMatrix.GetLength(0);
            var height = imageMatrix.GetLength(1);

            var radiusSX = convolutionMatrix.GetLength(0) / 2;

            var result = new double[width, height];
            var transposedMatrix = new double[convolutionMatrix.GetLength(0), convolutionMatrix.GetLength(1)];
            TransposeMatrix(convolutionMatrix, transposedMatrix);

            for (var x = radiusSX; x < width - radiusSX; x++)
                for (var y = radiusSX; y < height - radiusSX; y++)
                {
                    var gx = Convolve(convolutionMatrix, imageMatrix, x, y);
                    var gy = Convolve(transposedMatrix, imageMatrix, x, y);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }

            return result;
        }
    }
}