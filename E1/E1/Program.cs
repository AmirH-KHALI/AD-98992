using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Exam1 {
    public class Program {
        public static void Main(string[] args) {
            int verticalNum = int.Parse(args[1]);
            int horizontalNum = int.Parse(args[2]);
            string imgPath = args[0];
            Image img = Utilities.LoadImage(imgPath);
            Color[,] colorArray = Utilities.ConvertImageToColorArray(img);
            colorArray = Solve(colorArray, verticalNum, 'v');
            colorArray = Solve(colorArray, horizontalNum, 'h');
            Console.WriteLine(Directory.GetCurrentDirectory());
            Console.WriteLine("Hello");
            Utilities.SavePhoto(colorArray, Directory.GetCurrentDirectory(), imgPath + "_test", 'H');
        }
        public static Color[,] Solve(Color[,] input, int reduction, char direction) {
            double[,] energies = ComputeEnergy(input);
            //for (int i = 0; i < )
            return input;
        }

        public static string[] Solve(string[] data) {
            int dimReduction = int.Parse(data[0].Split()[0]);
            char direction = char.Parse(data[0].Split()[1]);
            string imagePath = data[1];
            var img = Utilities.LoadImage(imagePath);
            var bmp = Utilities.ConvertImageToColorArray(img);
            var res = Solve(bmp, dimReduction, direction);
            Utilities.SavePhoto(res, imagePath, "../../../../asd", direction);
            return Utilities.ConvertColorArrayToRGBMatrix(res);
        }

        private static List<List<Color>> BuildList(Color[,] input, char dim) {
            throw new NotImplementedException();
        }




        // sequence of indices for horizontal seam
        public static int[] findHorizontalSeam(List<List<double>> energy) {
            throw new NotImplementedException();
        }


        // sequence of indices for vertical seam
        public static int[] findVerticalSeam(List<List<double>> energy) {
            throw new NotImplementedException();
        }

        // energy of pixel at column x and row y
        public static double[, ] ComputeEnergy(Color[, ] bmp) {
            Pixel[,] pixels = new Pixel[bmp.GetLength(0), bmp.GetLength(1)];
            for (int i = 0; i < bmp.GetLength(0); ++i) {
                for (int j = 0; j < bmp.GetLength(1); ++j) {
                    pixels[i, j] = new Pixel(bmp[i, j].R, bmp[i, j].G, bmp[i, j].B);
                }
            }
            return Q3SeamCarving1.Solve(bmp.GetLength(0), bmp.GetLength(1), pixels);
        }

        public static int ArgMin(int t, int minIdx, List<List<double>> energy) {
            throw new NotImplementedException();
        }

        // remove horizontal seam from current picture
        public static void removeHorizontalSeam(int[] seam, ref List<List<Color>> bmp, ref List<List<double>> energy) {
            throw new NotImplementedException();
        }

        // remove vertical seam from current picture
        public static void removeVerticalSeam(int[] seam, ref List<List<Color>> bmp, ref List<List<double>> energy) {
            throw new NotImplementedException();
        }

    }
}
