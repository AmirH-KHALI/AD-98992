using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A9
{
    public class Q2OptimalDiet : Processor
    {
        public Q2OptimalDiet(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);

        public virtual string Solve(int n, int m, double[,] matrix1) {

            double[] values = new double[n];

            for (int i = 0; i < n; ++i) {
                values[i] = matrix1[i, m];
            }

            string[] allBinaries = Combine(m + n, m).ToArray();

            List<double[]> ans = new List<double[]>();
            double[,] matrix = new double[m, m + 1];
            int pos;

            for (int i = 0; i < allBinaries.Length; ++i) {
                string binary = allBinaries[i];
                pos = 0;
                for (int j = 0; j < binary.Length; ++j) {
                    if (binary[j] == '1') {
                        if (j >= n) {
                            for (int k = 0; k <= m; ++k) {
                                if (k == j - n) matrix[pos, k] = 1;
                                else matrix[pos, k] = 0;
                            }
                        } else {
                            for (int k = 0; k <= m; ++k) {
                                matrix[pos, k] = matrix1[j, k];
                            }
                        }
                        pos++;
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
                return "No Solution";
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
            StringBuilder finalAns = new StringBuilder();
            finalAns.Append("Bounded Solution\n");
            for (int i = 0; i < ansi.Length ; ++i) {
                double t = ansi[i];
                double abst = Math.Abs(t);
                if (abst - ((int)abst) < .25) abst = (int)abst;
                else if (((int)abst + 1) - abst < .25) abst = (int)abst + 1;
                else abst = (int)abst + .5;
                if (Math.Abs(t) == 0) ansi[i] = 0;
                else ansi[i] = (t / Math.Abs(t)) * abst;
                if (ansi[i] == -0) ansi[i] = 0;
                finalAns.Append(ansi[i] + " ");
            }
            return finalAns.ToString();
        }

        public string[] Combine(int m, int n) {
            List<string> ans = new List<string>();
            for (int i = (int)Math.Pow(2, n) - 1; i < Math.Pow(2, n) * Math.Pow(2, m - n); ++i) {
                string b = Convert.ToString(i, 2);
                if (ok(b, n))
                    ans.Add(zeroGen(m - b.Length) + b);
            }
            return ans.ToArray();
        }
        private string zeroGen(int n) {
            StringBuilder t = new StringBuilder();
            for (int i = 0; i < n; ++i)
                t.Append("0");
            return t.ToString();
        }

        private bool ok(string b, int n) {
            int ted = 0;
            for (int i = 0; i < b.Length; ++i) {
                if (b[i] == '1')
                    ted++;
            }
            if (ted == n)
                return true;
            return false;
        }

        private bool Check(double[] result, double[,] matrix1, double[] values, int m, int n) {
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


        public double[] Q1Solve(long n, double[,] matrix) {
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
