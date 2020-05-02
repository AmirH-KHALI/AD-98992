using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TestCommon;
using System.Text;
using Priority_Queue;

namespace Exam1 {
    public class Program {
        public static void Main(string[] args) {
            int verticalNum = int.Parse(args[1]);
            int horizontalNum = int.Parse(args[2]);
            string imgName = args[0];
            Console.WriteLine(imgName);
            Image img = Utilities.LoadImage(imgName);

            Color[,] colorArray = Utilities.ConvertImageToColorArray(img);
            Console.WriteLine(colorArray.GetLength(0) + " " + colorArray.GetLength(1));
            colorArray = Solve(colorArray, verticalNum, 0);//v
            colorArray = Solve(colorArray, horizontalNum, 1);//h
            Console.WriteLine(colorArray.GetLength(0) + " " + colorArray.GetLength(1));
            Utilities.SavePhoto(colorArray, Directory.GetCurrentDirectory(), imgName.Split('.')[0] + "_test", 'H');
        }
        public static Color[,] Solve(Color[,] input, int reduction, int direction) {
            double[,] energies = ComputeEnergy(input);
            List<List<string>> ansi = new List<List<string>>();
            List<List<Color>> colors = new List<List<Color>>();
            for (int i = 0; i < energies.GetLength(0); ++i) {
                ansi.Add(new List<string>());
                colors.Add(new List<Color>());
                for (int j = 0; j < energies.GetLength(1); ++j) {
                    ansi[i].Add(energies[i, j] + "");
                    colors[i].Add(input[i, j]);
                }
            }

            //for (int i = 0; i < colors.Count; ++i) {
            //    Console.WriteLine(ansi[1][i]);
            //}

            for (int k = 0; k < reduction; ++k) {
                string ans = Q3SeamCarving2.Solve(energies.GetLength(0), energies.GetLength(1), energies);
                List<List<long>> removeList = new List<List<long>>();
                removeList.Add(new List<long>());
                string[] temp = ans.Split('\n')[direction].Split(',');
                foreach (string s in temp) {
                    removeList[0].Add(int.Parse(s));
                }
                Console.WriteLine(((double)k / 100) + "% " + ansi.Count + " " + ansi[0].Count);
                if (direction == 0) {
                    ansi =  customSolve(ansi, removeList, new List<List<long>>());
                    colors = removeColors(colors, removeList, new List<List<long>>());
                } else {
                    ansi = customSolve(ansi, new List<List<long>>(), removeList);
                    colors = removeColors(colors, new List<List<long>>(), removeList);
                }
                energies = new double[ansi.Count, ansi[0].Count];
                for (int i = 0; i < ansi.Count; ++i) {
                    for (int j = 0; j < ansi[i].Count; ++j) {
                        energies[i, j] = double.Parse(ansi[i][j]);
                    }
                }
            }

            input = new Color[colors.Count, colors[0].Count];
            for (int i = 0; i < colors.Count; ++i) {
                for (int j = 0; j < colors[i].Count; ++j) {
                    input[i, j] = colors[i][j];
                }
            }

            return input;
        }

        public static List<List<string>> customSolve(List<List<string>> temp, List<List<long>> v, List<List<long>> h) {
            List<List<string>> ans;
            for (int i = 0; i < v.Count; ++i) {
                ans = new List<List<string>>();
                for (int j = 0; j < temp.Count; ++j) {
                    ans.Add(new List<string>());
                    for (int k = 0; k < temp[j].Count; ++k) {
                        if (v[i][j] != k) {
                            ans[j].Add(temp[j][k]);
                        }
                    }
                }
                temp = ans;
            }
            List<List<string>> tempi = new List<List<string>>();
            for (int i = 0; i < temp[0].Count; ++i) {
                tempi.Add(new List<string>());
                for (int j = 0; j < temp.Count; ++j) {
                    tempi[i].Add(temp[j][i]);
                }
            }
            for (int i = 0; i < h.Count; ++i) {
                ans = new List<List<string>>();
                for (int j = 0; j < tempi.Count; ++j) {
                    ans.Add(new List<string>());
                    for (int k = 0; k < tempi[j].Count; ++k) {
                        if (h[i][j] != k) {
                            ans[j].Add(tempi[j][k]);
                        }
                    }
                }
                tempi = ans;
            }
            List<List<string>> tempii = new List<List<string>>();
            for (int i = 0; i < tempi[0].Count; ++i) {
                tempii.Add(new List<string>());
                for (int j = 0; j < tempi.Count; ++j) {
                    tempii[i].Add(tempi[j][i]);
                }
            }
            return tempii;

        }
        public static List<List<Color>> removeColors(List<List<Color>> temp, List<List<long>> v, List<List<long>> h) {

            List<List<Color>> ans;
            for (int i = 0; i < v.Count; ++i) {
                ans = new List<List<Color>>();
                for (int j = 0; j < temp.Count; ++j) {
                    ans.Add(new List<Color>());
                    for (int k = 0; k < temp[j].Count; ++k) {
                        if (v[i][j] != k) {
                            ans[j].Add(temp[j][k]);
                        }
                    }
                }
                temp = ans;
            }
            List<List<Color>> tempi = new List<List<Color>>();
            for (int i = 0; i < temp[0].Count; ++i) {
                tempi.Add(new List<Color>());
                for (int j = 0; j < temp.Count; ++j) {
                    tempi[i].Add(temp[j][i]);
                }
            }
            for (int i = 0; i < h.Count; ++i) {
                ans = new List<List<Color>>();
                for (int j = 0; j < tempi.Count; ++j) {
                    ans.Add(new List<Color>());
                    for (int k = 0; k < tempi[j].Count; ++k) {
                        if (h[i][j] != k) {
                            ans[j].Add(tempi[j][k]);
                        }
                    }
                }
                tempi = ans;
            }
            List<List<Color>> tempii = new List<List<Color>>();
            for (int i = 0; i < tempi[0].Count; ++i) {
                tempii.Add(new List<Color>());
                for (int j = 0; j < tempi.Count; ++j) {
                    tempii[i].Add(tempi[j][i]);
                }
            }
            return tempii;

        }

        /*public static string[] Solve(string[] data) {
            int dimReduction = int.Parse(data[0].Split()[0]);
            char direction = char.Parse(data[0].Split()[1]);
            string imagePath = data[1];
            var img = Utilities.LoadImage(imagePath);
            var bmp = Utilities.ConvertImageToColorArray(img);
            var res = Solve(bmp, dimReduction, direction);
            Utilities.SavePhoto(res, imagePath, "../../../../asd", direction);
            return Utilities.ConvertColorArrayToRGBMatrix(res);
        }*/

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
