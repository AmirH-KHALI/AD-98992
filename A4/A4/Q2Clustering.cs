using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using static A4.Q1BuildingRoads;
using Priority_Queue;

namespace A4
{
    public class Q2Clustering : Processor
    {
        public Q2Clustering(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, double>)Solve);

        public double Solve(long pointCount, long[][] points, long clusterCount)
        {
            return prim(pointCount, points, clusterCount);
        }

        private double prim(long pointCount, long[][] points, long clusterCount) {

            double ans = 0;

            double[] dist = new double[pointCount];
            bool[] mark = new bool[pointCount];

            SimplePriorityQueue<long> q = new SimplePriorityQueue<long>();
            SimplePriorityQueue<long> treeEdges = new SimplePriorityQueue<long>();

            for (int i = 0; i < pointCount; ++i) {
                if (i == 0) dist[i] = 0;
                else dist[i] = int.MaxValue;
                q.Enqueue(i, (float)dist[i]);
            }

            while (q.Count > 0) {
                long cPoint = q.Dequeue();
                mark[cPoint] = true;

                treeEdges.Enqueue(cPoint, (float)(dist[cPoint]*(-1)));
                
                for (int i = 0; i < points.Length; ++i) {
                    if (!mark[i]) {
                        dist[i] = Math.Min(dist[i], distBetween(cPoint, i, points));
                        q.UpdatePriority(i, (float)dist[i]);
                    }
                }
            }

            for (int i = 0; i < clusterCount - 1; ++i) {
                ans = dist[treeEdges.Dequeue()];
            }

            return double.Parse(ans.ToString("#.######"));

        }

        private double distBetween(long x, int y, long[][] points) {
            long first2 = points[x][0] - points[y][0];
            first2 *= first2;
            long second2 = points[x][1] - points[y][1];
            second2 *= second2;
            return Math.Sqrt(first2 + second2);
        }
    }
}
