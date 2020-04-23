using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using TestCommon;
using System.Drawing;
using Priority_Queue;

namespace Exam1
{
    public class Pixel {
        public int r;
        public int g;
        public int b;
        public Pixel(int r, int g, int b) {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }
    public class Q3SeamCarving1 : Processor // Calculate Energy
    {
        public Q3SeamCarving1(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string>)Solve);

        

        public static Tuple<int, int, Pixel[,]> ProcessQ31(string[] data) {
            Pixel[,] pixels = new Pixel[data.Length, data[0].Split('|').Length];
            for (int i = 0; i < data.Length; ++i) {
                string[] temp = data[i].Split('|');
                for (int j = 0; j < temp.Length; ++j) {
                    string[] tempi = temp[j].Split(',');
                    pixels[i, j] = (new Pixel(int.Parse(tempi[0]), int.Parse(tempi[1]), int.Parse(tempi[2])));
                }
            }
            return Tuple.Create(data.Length, data[0].Split('|').Length, pixels);
        }
        public string Solve(string[] input) {
            Tuple<int, int, Pixel[,]> data = ProcessQ31(input);
            int n = data.Item1;
            int m = data.Item2;
            double[,] ans = Solve(n, m, data.Item3);
            string ansi = "";
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < m; ++j) {
                    ansi += ans[i, j].ToString("#.###"); ;
                    if (j < m - 1) ansi += ',';
                }
                if (i < n - 1) ansi += '\n';
            }
            return ansi;
        }


        public double[,] Solve(int n, int m, Pixel[,] data) {
            double[,] ans = new double[n, m];
            for (int i = 0; i < n; ++i) {
                for (int j = 0; j < m; ++j) {
                    if (i == 0 || j == 0 || i == n - 1 || j == m - 1) {
                        ans[i, j] = 1000;
                    } else {
                        double Rx, Gx, Bx, Ry, Gy, By, deltaX2, deltaY2;
                        Rx = data[(i + 1) % n, j].r - data[(i - 1 + n) % n, j].r;
                        Gx = data[(i + 1) % n, j].g - data[(i - 1 + n) % n, j].g;
                        Bx = data[(i + 1) % n, j].b - data[(i - 1 + n) % n, j].b;
                        Ry = data[i, (j + 1) % m].r - data[i, (j - 1 + m) % m].r;
                        Gy = data[i, (j + 1) % m].g - data[i, (j - 1 + m) % m].g;
                        By = data[i, (j + 1) % m].b - data[i, (j - 1 + m) % m].b;
                        deltaX2 = Rx * Rx + Gx * Gx + Bx * Bx;
                        deltaY2 = Ry * Ry + Gy * Gy + By * By;
                        ans[i, j] = Math.Sqrt(deltaX2 + deltaY2);
                    }
                }
            }
            return ans;
        }
    }

    public class Q3SeamCarving2 : Processor // Find Seam
    {
        public Q3SeamCarving2(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string>)Solve);


        public static Tuple<int, int, double[,]> ProcessQ32(string[] data) {
            double[,] ans = new double[data.Length, data[0].Split(',').Length];
            for (int i = 0; i < data.Length; ++i) {
                string[] temp = data[i].Split(',');
                for (int j = 0; j < temp.Length; ++j) {
                    ans[i, j] = double.Parse(temp[j]);
                }
            }
            return Tuple.Create(data.Length, data[0].Split(',').Length, ans);
        }
        public string Solve(string[] input) {
            Tuple<int, int, double[,]> data = ProcessQ32(input);
            int n = data.Item1;
            int m = data.Item2;
            return Solve(n, m, data.Item3);
        }

        public string Solve(int n, int m, double[,] data)
        {
            List<long> hor = prim(n, m, data);
            List<long> ver = prim2(n, m, data);
            string ans = "";
            hor.Reverse();
            ver.Reverse();
            for (int i = 0; i < ver.Count; ++i) {
                if (i == 0) ans += ver[i + 1].ToString();
                else if (i == ver.Count - 1) ans += ver[i - 1].ToString();
                else ans += ver[i].ToString();
                if (i == ver.Count - 1) {
                    ans += '\n';
                } else ans += ',';
            }
            for (int i = 0; i < hor.Count; ++i) {
                if (i == 0) ans += hor[i + 1].ToString();
                else if (i == hor.Count - 1) ans += hor[i - 1].ToString();
                else ans += hor[i].ToString();
                if (i != hor.Count - 1) ans += ',';
            }
            return ans;
        }

        private List<long> prim2(int n, int m, double[,] data) {

            double[] dist = new double[n * m];
            bool[] mark = new bool[n * m];
            long[] par = new long[n * m];

            SimplePriorityQueue<long> q = new SimplePriorityQueue<long>();

            for (int i = 0; i < m * n; ++i) {
                par[i] = -1;
                if (i / m == 0) dist[i] = 0;
                else dist[i] = int.MaxValue;
                q.Enqueue(i, (float)dist[i]);
            }

            List<long> ans = new List<long>();

            while (q.Count > 0) {
                long cPoint = q.Dequeue();
                mark[cPoint] = true;
                long cI = cPoint / m;
                long cJ = cPoint % m;

                if (cI == n - 1) {
                    while (cPoint != -1) {
                        ans.Add(cPoint % m);
                        cPoint = par[cPoint];
                    }
                    return ans;
                }

                for (int i = -1; i <= 1; ++i) {
                    long childI = cI + 1;
                    long childJ = cJ + i;
                    if (childJ < 0 || childJ >= m) continue;
                    long childPoint = childI * m + childJ;
                    if (!mark[childPoint]) {
                        dist[childPoint] = Math.Min(dist[childPoint], dist[cPoint] + data[childI, childJ]);
                        if (dist[childPoint] == dist[cPoint] + data[childI, childJ])
                            par[childPoint] = cPoint;
                        q.UpdatePriority(childPoint, (float)dist[childPoint]);
                    }
                }
            }
            return ans;
        }

        private List<long> prim(int n, int m, double[,] data) {

            double[] dist = new double[n * m];
            bool[] mark = new bool[n * m];
            long[] par = new long[n * m];

            SimplePriorityQueue<long> q = new SimplePriorityQueue<long>();

            for (int i = 0; i < m * n; ++i) {
                par[i] = -1;
                if (i % m == 0) dist[i] = 0;
                else dist[i] = int.MaxValue;
                q.Enqueue(i, (float)dist[i]);
            }

            List<long> ans = new List<long>();

            while (q.Count > 0) {
                long cPoint = q.Dequeue();
                mark[cPoint] = true;
                long cI = cPoint / m;
                long cJ = cPoint % m;

                if (cJ == m - 1) {
                    while (cPoint != -1) {
                        ans.Add(cPoint / m);
                        cPoint = par[cPoint];
                    }
                    return ans;
                }

                for (int i = -1; i <= 1; ++i) {
                    long childI = cI + i;
                    long childJ = cJ + 1;
                    if (childI < 0 || childI >= n) continue;
                    long childPoint = childI * m + childJ;
                    if (!mark[childPoint]) {
                        dist[childPoint] = Math.Min(dist[childPoint], dist[cPoint] + data[childI, childJ]);
                        if (dist[childPoint] == dist[cPoint] + data[childI, childJ])
                            par[childPoint] = cPoint;
                        q.UpdatePriority(childPoint, (float)dist[childPoint]);
                    }
                }
            }
            return ans;
        }

    }

    public class Q3SeamCarving3 : Processor // Remove Seam
    {
        public Q3SeamCarving3(string testDataName) : base(testDataName) {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string>)Solve);


        public static Tuple<int, int, string[,], List<List<long>>, List<List<long>>> ProcessQ33(string[] data) {
            string[,] ans = new string[int.Parse(data[0]), data[1].Split(',').Length];
            int i = 1;
            string[] temp;
            while ((temp = data[i].Split(',')).Length > 1) {
                for (int j = 0; j < temp.Length; ++j) {
                    ans[i - 1, j] = temp[j];
                }
                ++i;
            }
            int q = int.Parse(data[i]);
            i++;
            List<List<long>> v = new List<List<long>>();
            List<List<long>> h = new List<List<long>>();
            for (; i < data.Length; ++i) {
                temp = data[i].Split(':');
                string[] tempi = temp[1].Split(',');
                if (temp[0] == "v") {
                    v.Add(new List<long>());
                    for (int j = 0; j < tempi.Length; ++j) {
                        v[v.Count - 1].Add(int.Parse(tempi[j]));
                    }
                }
                else {
                    h.Add(new List<long>());
                    for (int j = 0; j < tempi.Length; ++j) {
                        h[h.Count - 1].Add(int.Parse(tempi[j]));
                    }
                }
            }
            return Tuple.Create(int.Parse(data[0]), data[1].Split(',').Length, ans, v, h);
        }
        public string Solve(string[] input) {
            Tuple<int, int, string[,], List<List<long>>, List<List<long>>> data = ProcessQ33(input);
            int n = data.Item1;
            int m = data.Item2;
            return Solve(n, m, data.Item3, data.Item4, data.Item5);
        }


        public string Solve(int n, int m, string[,] data, List<List<long>> v, List<List<long>> h) {
            List<List<string>> temp = new List<List<string>>();
            for (int i = 0; i < n; ++i) {
                temp.Add(new List<string>());
                for (int j = 0; j < m; ++j) {
                    temp[i].Add(data[i, j]);
                }
            }
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
            for (int i = 0; i < h.Count; ++i) {
                ans = new List<List<string>>();
                for (int j = 0; j < temp.Count; ++j) {
                    ans.Add(new List<string>());
                    for (int k = 0; k < temp[j].Count; ++k) {
                        if (h[i][k] != j) {
                            ans[j].Add(temp[j][k]);
                        }
                    }
                }
                temp = ans;
            }
            string ansi = "";
            for (int i = 0; i < temp.Count; ++i) {
                for (int j = 0; j < temp[i].Count; ++j) {
                    if (temp[i][j] == "1000") {
                        ansi += "1000.00";
                    }
                    else ansi += temp[i][j];
                    if (j < temp[i].Count - 1) ansi += ',';
                }
                if (i < temp.Count - 1) ansi += '\n';
            }
            return ansi;
        }
    }
}
