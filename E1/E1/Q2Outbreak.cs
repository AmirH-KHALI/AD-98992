using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;
using Priority_Queue;

namespace Exam1
{
    public class Q2Outbreak : Processor
    {
        public Q2Outbreak(string testDataName) : base(testDataName) {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string>)Solve);

        public static Tuple<int, int, int[,], int[,]> ProcessQ2(string[] data)
        {
            var temp = data[0].Split();
            int N = int.Parse(temp[0]);
            int M = int.Parse(temp[1]);
            int[,] carriers = new int[N, 2];
            int[,] safe = new int[M, 2];
            for (int i = 0; i < N; i++)
            {
                carriers[i, 0] = int.Parse(data[i + 1].Split()[0]);
                carriers[i, 1] = int.Parse(data[i + 1].Split()[1]);
            }

            for (int i = 0; i < M; i++)
            {
                safe[i, 0] = int.Parse(data[i + N + 1].Split()[0]);
                safe[i, 1] = int.Parse(data[i + N + 1].Split()[1]);
            }
            return Tuple.Create(N, M, carriers, safe);
        }
        public string Solve(string[] input)
        {
            var data = ProcessQ2(input);
            return Solve(data.Item1,data.Item2,data.Item3,data.Item4).ToString();
        }
        public double Solve(int N, int M, int[,] carrier, int[,] safe)
        {
            List<long>[] points = new List<long>[2];
            points[0] = new List<long>();
            points[1] = new List<long>();
            for (int i = 0; i < N; ++i) {
                points[0].Add((long)carrier[i, 0]);
                points[1].Add((long)carrier[i, 1]);
            }
            for (int i = 0; i < M; ++i) {
                points[0].Add(safe[i, 0]);
                points[1].Add(safe[i, 1]);
            }

            return prim(M + N, points[0].ToArray(), points[1].ToArray());
        }

        private double prim(long pointCount, long[] X, long[] Y) {

            double ans = 0;

            double[] dist = new double[pointCount];
            bool[] mark = new bool[pointCount];

            SimplePriorityQueue<long> q = new SimplePriorityQueue<long>();

            for (int i = 0; i < pointCount; ++i) {
                if (i == 0) dist[i] = 0;
                else dist[i] = int.MaxValue;
                q.Enqueue(i, (float)dist[i]);
            }

            while (q.Count > 0) {
                long cPoint = q.Dequeue();
                mark[cPoint] = true;

                ans = Math.Max(ans, dist[cPoint]);

                for (int i = 0; i < X.Length; ++i) {
                    if (!mark[i]) {
                        dist[i] = Math.Min(dist[i], distBetween(cPoint, i, X, Y));
                        q.UpdatePriority(i, (float)dist[i]);
                    }
                }
            }

            return double.Parse(ans.ToString("#.######"));

        }

        private double distBetween(long x, int y, long[] X, long[] Y) {
            long first2 = X[x] - X[y];
            first2 *= first2;
            long second2 = Y[x] - Y[y];
            second2 *= second2;
            return Math.Sqrt(first2 + second2);
        }
    }
}
