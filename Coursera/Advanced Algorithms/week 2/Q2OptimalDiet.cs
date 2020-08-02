using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Diet {
    class Program {
        static void Main(string[] args) {
            string[] nm = Console.ReadLine().Split();
            int n = int.Parse(nm[0]);
            int m = int.Parse(nm[1]);
            double[,] matrix1 = new double[n + 1, m + 1];
            for (int i = 0; i < n; ++i) {
                string[] tmp = Console.ReadLine().Split();
                for (int j = 0; j < tmp.Length; ++j) {
                    matrix1[i, j] = double.Parse(tmp[j]);
                }
            }
            string[] temp = Console.ReadLine().Split();
            for (int i = 0; i < temp.Length; ++i) {
                matrix1[i, m] = double.Parse(temp[i]);
            }
            temp = Console.ReadLine().Split();
            for (int i = 0; i < temp.Length; ++i) {
                matrix1[n, i] = double.Parse(temp[i]);
            }
            Solve(n, m, matrix1);
        }
        public static void Solve(int n, int m, double[,] matrix1) {

            double[] values = new double[n];

            for (int i = 0; i < n; ++i) {
                values[i] = matrix1[i, m];
            }

            string[] allBinaries = Combine(m + n, m).ToArray();

            double[,] matrix = new double[m, m + 1];
            int x;
            List<double[]> ans = new List<double[]>();

            for (int i = 0; i < allBinaries.Length; ++i) {
                string binary = allBinaries[i];
                x = 0;
                for (int j = 0; j < binary.Length; ++j) {
                    if (binary[j] == '1') {
                        if (j >= n) {
                            for (int k = 0; k <= m; ++k) {
                                if (k == j - n) matrix[x, k] = 1;
                                else matrix[x, k] = 0;
                            }
                        } else {
                            for (int k = 0; k <= m; ++k) {
                                matrix[x, k] = matrix1[j, k];
                            }
                        }
                        x++;
                    }
                }
                double[] ansii = Q1Solve(m, matrix);
                if (ansii.Length != m + 1) {
                    if (Check(ansii, matrix1, values, m, n)) {
                        ans.Add(ansii);
                    }
                }
            }
            if (ans.Count == 0) {
                Console.WriteLine("No solution");
                return;
            }
            double max = double.MinValue;
            int index = 0;
            for (int i = 0; i < ans.Count; ++i) {
                double d = 0;
                for (int j = 0; j < m; ++j) {
                    d += matrix1[n, j] * ans[i][j];
                }
                if (d > max) {
                    max = d;
                    index = i;
                }
            }
            double[] ansi = ans[index];
            double sum = 0;
            for (int i = 0; i < ansi.Length; ++i) {
                sum += ansi[i];
            }
            if (sum + 1e-3 >= 1e9) {
                Console.WriteLine("Infinity");
                return;
            }
            Console.WriteLine("Bounded solution");
            for (int i = 0; i < ansi.Length - 1; ++i) {
                if (ansi[i] == -0) Console.Write("-" + String.Format("{0:0.000000000000000000}", ansi[i]) + " ");
                else Console.Write(String.Format("{0:0.000000000000000000}", ansi[i]) + " ");
            }
            if (ansi.Length > 0) {
                int i = ansi.Length - 1;
                if (ansi[i] == -0) Console.Write("-" + String.Format("{0:0.000000000000000000}", ansi[i]));
                else Console.Write(String.Format("{0:0.000000000000000000}", ansi[i]));
            }
        }

        public static string[] Combine(int m, int n) {
            List<string> ans = new List<string>();
            for (int i = (int)Math.Pow(2, n) - 1; i < Math.Pow(2, n) * Math.Pow(2, m - n); ++i) {
                string b = Convert.ToString(i, 2);
                if (ok(b, n))
                    ans.Add(zeroGen(m - b.Length) + b);
            }
            return ans.ToArray();
        }
        private static string zeroGen(int n) {
            StringBuilder t = new StringBuilder();
            for (int i = 0; i < n; ++i)
                t.Append("0");
            return t.ToString();
        }

        private static bool ok(string b, int n) {
            int ted = 0;
            for (int i = 0; i < b.Length; ++i) {
                if (b[i] == '1')
                    ted++;
            }
            if (ted == n)
                return true;
            return false;
        }

        private static bool Check(double[] result, double[,] matrix1, double[] values, int m, int n) {
            foreach (double num in result)
                if (num < 0)
                    return false;
            for (int i = 0; i < n; i++) {
                double res = 0;
                for (int j = 0; j < m; j++)
                    res += matrix1[i, j] * result[j];
                if (res - values[i] >= 0.0001)
                    return false;
            }
            return true;
        }


        public static double[] Q1Solve(long n, double[,] matrix) {
            bool[] mark = new bool[matrix.GetLength(0)]; //lodum satr ha 1 shodan

            long[] order = new long[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(1) - 1; ++i) { // sefr kardan e sotun i om
                for (int j = 0; j < matrix.GetLength(0); ++j) { // peyda kardan satr qeir e sefr
                    if (!mark[j] && matrix[j, i] != 0) {
                        mark[j] = true;
                        order[i] = j;
                        double temp = matrix[j, i];
                        for (int k = i; k < matrix.GetLength(1); ++k) {
                            matrix[j, k] /= temp;
                        }
                        for (int k = 0; k < matrix.GetLength(0); ++k) {
                            if (k == j) continue;
                            double mazrab = matrix[k, i] / matrix[j, i];
                            for (int l = i; l < matrix.GetLength(1); ++l) {
                                matrix[k, l] -= matrix[j, l] * mazrab;
                            }
                        }
                        break;
                    }
                }
            }

            double[] ans = new double[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); ++i) {
                double t = matrix[order[i], matrix.GetLength(1) - 1];
                double abst = Math.Abs(t);
                //if (abst - ((int)abst) < .25) abst = (int)abst;
                //else if (((int)abst + 1) - abst < .25) abst = (int)abst + 1;
                //else abst = (int)abst + .5;
                if (Math.Abs(t) == 0) ans[i] = 0;
                else ans[i] = (t / Math.Abs(t)) * abst;
            }
            return ans;
        }
    }
}