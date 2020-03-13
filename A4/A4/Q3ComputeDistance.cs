using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using GeoCoordinatePortable;
using Priority_Queue;

namespace A4
{
    public class Q3ComputeDistance : Processor
    {
        public Q3ComputeDistance(string testDataName) : base(testDataName) { }

        public static readonly char[] IgnoreChars = new char[] { '\n', '\r', ' ' };
        public static readonly char[] NewLineChars = new char[] { '\n', '\r' };

        private static double[][] ReadTree(IEnumerable<string> lines)
        {
            return lines.Select(line => 
                line.Split(IgnoreChars, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(n => double.Parse(n)).ToArray()
                            ).ToArray();
        }
        public override string Process(string inStr)
        {
            //ExcludeTestCaseRangeInclusive(1, 11);
            return Process(inStr, (Func<long, long, double[][], double[][], long,
                                    long[][], double[]>)Solve);
        }
        public static string Process(string inStr, Func<long, long, double[][]
                                  ,double[][], long, long[][], double[]> processor)
        {

            var lines = inStr.Split(NewLineChars, StringSplitOptions.RemoveEmptyEntries);
            long[] count = lines.First().Split(IgnoreChars,
                                                StringSplitOptions.RemoveEmptyEntries)
                                        .Select(n => long.Parse(n))
                                        .ToArray();
            double[][] points = ReadTree(lines.Skip(1).Take((int)count[0]));
            double[][] edges = ReadTree(lines.Skip(1 + (int)count[0]).Take((int)count[1]));
            long queryCount = long.Parse(lines.Skip(1 + (int)count[0] + (int)count[1]) 
                                            .Take(1).FirstOrDefault());
            long[][] queries = ReadTree(lines.Skip(2 + (int)count[0] + (int)count[1]))
                                        .Select(x => x.Select(z => (long)z).ToArray())
                                        .ToArray();

            return string.Join("\n", processor(count[0], count[1], points, edges,
                                queryCount, queries));
        }
        
        private GeoCoordinate[] geoPoints;
        private double[][] p;

        public double[] Solve (long nodeCount, long edgeCount, double[][] points,
                               double[][] edges, long queriesCount, long[][] queries) {

            geoPoints = new GeoCoordinate[nodeCount];
            for (int i = 0; i < nodeCount; ++i) {
                (points[i][0], points[i][1]) = (points[i][1], points[i][0]);
                geoPoints[i] = new GeoCoordinate(points[i][0], points[i][1]);
            }

            p = points;

            double[] ans = new double[queries.Length];
            List<Tuple<long, double>>[]  adj = new List<Tuple<long, double>>[nodeCount];
            List<Tuple<long, double>>[] radj = new List<Tuple<long, double>>[nodeCount];

            for (int i = 0; i < nodeCount; ++i) {
                 adj[i] = new List<Tuple<long, double>>();
                radj[i] = new List<Tuple<long, double>>();
            }

            for (int i = 0; i < edges.Length; ++i) {
                adj [(long)edges[i][0] - 1].Add(new Tuple<long, double>((long)edges[i][1] - 1, edges[i][2]));
                radj[(long)edges[i][1] - 1].Add(new Tuple<long, double>((long)edges[i][0] - 1, edges[i][2]));
            }

            double[] distStart = new double[nodeCount];
            double[] distEnd   = new double[nodeCount];

            SimplePriorityQueue<long> minHeapStart = new SimplePriorityQueue<long>();
            SimplePriorityQueue<long> minHeapEnd   = new SimplePriorityQueue<long>();


            List<long> markStart = new List<long>();
            List<long> markEnd   = new List<long>();


            for (int i = 0; i < queries.Length; ++i) {
                queries[i][0]--;
                queries[i][1]--;
                ans[i] = Dijkstra(queries[i][0],
                                  queries[i][1],
                                  adj,
                                  radj,
                                  nodeCount,
                                  distStart,
                                  distEnd,
                                  minHeapStart,
                                  minHeapEnd,
                                  markStart,
                                  markEnd);
            }
            return ans;
        }

        private double Dijkstra (long startNode, long endNode,
                                List<Tuple<long, double>>[] adj, List<Tuple<long, double>>[] radj,
                                long nodeCount, double[] distStart, double[] distEnd,
                                SimplePriorityQueue<long> minHeapStart,
                                SimplePriorityQueue<long> minHeapEnd,
                                List<long> markStart, List<long> markEnd) {

            minHeapStart.Clear();
            minHeapEnd.Clear();
            markStart.Clear();
            markEnd.Clear();

            for (int i = 0; i < nodeCount; ++i) {
                distEnd  [i] = int.MaxValue;
                distStart[i] = int.MaxValue;
                if (i == endNode) {
                    distEnd[i] = 0;
                    minHeapEnd.Enqueue(i, (float)distEnd[i] + (float)getH(i, startNode));
                }
                if (i == startNode) {
                    distStart[i] = 0;
                    minHeapStart.Enqueue(i, (float)distStart[i] + (float)getH(i, endNode));
                }
                //minHeapEnd  .Enqueue(i, (float)distEnd  [i] + (float)getH(i, startNode));
                //minHeapStart.Enqueue(i, (float)distStart[i] + (float)getH(i, endNode  ));
                }

            while (minHeapStart.Count > 0 && minHeapEnd.Count > 0) {

                long current = minHeapStart.Dequeue();
                markStart.Add(current);
                for (long i = 0; i < adj[current].Count; ++i) {
                    Tuple<long, double> child = adj[current][(int)i];
                    if (distStart[child.Item1] > distStart[current] + child.Item2) {
                        distStart[child.Item1] = distStart[current] + child.Item2;
                        if (minHeapStart.Contains(child.Item1))
                            minHeapStart.UpdatePriority(child.Item1, (float)distStart[child.Item1] +
                                                                     (float)getH(child.Item1, endNode));
                        else
                            minHeapStart.Enqueue(child.Item1, (float)distStart[child.Item1] +
                                                              (float)getH(child.Item1, endNode));
                    }
                }

                if (markEnd.Contains(current)) break;

                current = minHeapEnd.Dequeue();
                markEnd.Add(current);
                for (long i = 0; i < radj[current].Count; ++i) {
                    Tuple<long, double> child = radj[current][(int)i];
                    if (distEnd[child.Item1] > distEnd[current] + child.Item2) {
                        distEnd[child.Item1] = distEnd[current] + child.Item2;
                        if (minHeapEnd.Contains(child.Item1))
                            minHeapEnd.UpdatePriority(child.Item1, (float)distEnd[child.Item1] +
                                                                   (float)getH(startNode, child.Item1));
                        else
                            minHeapEnd.Enqueue(child.Item1, (float)distEnd[child.Item1] +
                                                            (float)getH(startNode, child.Item1));
                    }
                }

                if (markStart.Contains(current)) break;
            }

            double ans = int.MaxValue;
            for (int i = 0; i < markStart.Count; ++i) {
                long j = markStart[i];
                if (distStart[j] < int.MaxValue && distEnd[j] < int.MaxValue &&
                    distStart[j] + distEnd[j] < ans) {
                    ans = distStart[j] + distEnd[j];
                }
            }
            for (int i = 0; i < markEnd.Count; ++i) {
                long j = markEnd[i];
                if (distStart[j] < int.MaxValue && distEnd[j] < int.MaxValue &&
                    distStart[j] + distEnd[j] < ans) {
                    ans = distStart[j] + distEnd[j];
                }
            }
            if (ans == int.MaxValue) ans = -1;
            return ans;
        }

        private double getH (long i, long j) {
            //*
            if ((long)p[i][0] == p[i][0]) {
                
                return Math.Abs(p[i][0] - p[j][0]) + Math.Abs(p[i][1] - p[j][1]);
                
            }
            //*/
            //GeoCoordinate g1 = new GeoCoordinate(p[i][0], p[i][1]);
            //GeoCoordinate g2 = new GeoCoordinate(p[j][0], p[j][1]);
            return geoPoints[i].GetDistanceTo(geoPoints[j]);
            //*/
        }

    }
}