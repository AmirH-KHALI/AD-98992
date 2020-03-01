using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q1MinCost : Processor
    {
        public Q1MinCost(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);


        public long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
        {
            List<Tuple<long, long>>[] adj = new List<Tuple<long, long>>[nodeCount];
            for (int i = 0; i < nodeCount; ++i) {
                adj[i] = new List<Tuple<long, long>>();
            }

            for (int i = 0; i < edges.Length; ++i) {
                adj[edges[i][0] - 1].Add(new Tuple<long, long>(edges[i][1] - 1, edges[i][2]));
            }

            return dijkstra(startNode - 1, endNode - 1, adj, nodeCount);
        }

        private long dijkstra(long startNode, long endNode, List<Tuple<long, long>>[] adj, long nodeCount) {
            
            long[] dist = new long[nodeCount];
            SortedSet<Tuple<long, long>> s = new SortedSet<Tuple<long, long>>();

            for (int i = 0; i < nodeCount; ++i) {
                if (i == startNode) dist[i] = 0;
                else dist[i] = int.MaxValue;
                s.Add(new Tuple<long, long>(dist[i], i));
            }

            while (s.Count > 0) {
                Tuple<long, long> current = s.Min();
                s.Remove(s.Min());
                //if (current.Item1 == long.MaxValue) break;
                for (long i = 0; i < adj[current.Item2].Count; ++i) {
                    Tuple<long, long> child = adj[current.Item2][(int)i];
                    if (dist[child.Item1] > current.Item1 + child.Item2) {
                        s.Remove(new Tuple<long, long>(dist[child.Item1], child.Item1));
                        dist[child.Item1] = current.Item1 + child.Item2;
                        s.Add(new Tuple<long, long>(dist[child.Item1], child.Item1));
                    }
                }
            }

            if (dist[endNode] == int.MaxValue) dist[endNode] = -1;
            return dist[endNode];
        }
    }
}   