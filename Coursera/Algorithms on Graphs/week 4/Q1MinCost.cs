using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3 {
    public class Q1MinCost {
        static void Main(string[] args) {
            long[] t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long v = t[0];
            long e = t[1];
            long[][] edges = new long[e][];
            for (int i = 0; i < e; i++) {
                edges[i] = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            }
            t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            Console.WriteLine(Solve(v, edges, t[0], t[1]));
        }
        public static long Solve(long nodeCount, long[][] edges, long startNode, long endNode) {
            List<Tuple<long, long>>[] adj = new List<Tuple<long, long>>[nodeCount];
            for (int i = 0; i < nodeCount; ++i) {
                adj[i] = new List<Tuple<long, long>>();
            }

            for (int i = 0; i < edges.Length; ++i) {
                adj[edges[i][0] - 1].Add(new Tuple<long, long>(edges[i][1] - 1, edges[i][2]));
            }

            return dijkstra(startNode - 1, endNode - 1, adj, nodeCount);
        }

        private static long dijkstra(long startNode, long endNode, List<Tuple<long, long>>[] adj, long nodeCount) {

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