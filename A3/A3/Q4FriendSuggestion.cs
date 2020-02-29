using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q4FriendSuggestion:Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long,long[][], long[]>)Solve);

        public long[] Solve(long nodeCount, long edgeCount, 
                              long[][] edges, long queriesCount, 
                              long[][] queries) {
            
            long[] ans = new long[queries.Length];
            List<Tuple<long, long>>[] adj = new List<Tuple<long, long>>[nodeCount];
            for (int i = 0; i < nodeCount; ++i) {
                adj[i] = new List<Tuple<long, long>>();
            }

            for (int i = 0; i < edges.Length; ++i) {
                adj[edges[i][0] - 1].Add(new Tuple<long, long>(edges[i][1] - 1, edges[i][2]));
            }

            for (int i = 0; i < queries.Length; ++i) {
                queries[i][0]--;
                queries[i][1]--;
                ans[i] = dijkstra(queries[i][0], queries[i][1], adj, nodeCount);
            }
            return ans;
        }
        private long dijkstra(long startNode, long endNode, List<Tuple<long, long>>[] adj, long nodeCount) {

            long[] dist = new long[nodeCount];
            bool[] mark = new bool[nodeCount];
            SortedSet<Tuple<long, long>> s = new SortedSet<Tuple<long, long>>();

            for (int i = 0; i < nodeCount; ++i) {
                if (i == startNode) dist[i] = 0;
                else dist[i] = long.MaxValue;
                s.Add(new Tuple<long, long>(dist[i], i));
            }

            while (s.Count > 0) {
                Tuple<long, long> current = s.Min();
                s.Remove(s.Min());
                if (current.Item1 == long.MaxValue) break;
                for (long i = 0; i < adj[current.Item2].Count; ++i) {
                    Tuple<long, long> child = adj[current.Item2][(int)i];
                    if (!mark[child.Item1] && dist[child.Item1] > current.Item1 + child.Item2) {
                        s.Remove(new Tuple<long, long>(dist[child.Item1], child.Item1));
                        dist[child.Item1] = current.Item1 + child.Item2;
                        s.Add(new Tuple<long, long>(dist[child.Item1], child.Item1));
                    }
                }
            }

            if (dist[endNode] == long.MaxValue) dist[endNode] = -1;
            return dist[endNode];
        }
    }
}
