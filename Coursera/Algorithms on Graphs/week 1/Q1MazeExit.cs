using System;
using System.Collections.Generic;
using System.Linq;

namespace A1 {
    public class Q1MazeExit {
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
        public static long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode) {
            bool[] mark = new bool[nodeCount];
            List<long>[] adj = new List<long>[nodeCount];
            for (int i = 0; i < adj.Length; ++i) {
                adj[i] = new List<long>();
            }
            for (int i = 0; i < edges.Length; ++i) {
                adj[edges[i][0] - 1].Add(edges[i][1] - 1);
                adj[edges[i][1] - 1].Add(edges[i][0] - 1);
            }
            return dfs(StartNode - 1, EndNode - 1, adj, mark);
        }

        private static long dfs(long x, long goal, List<long>[] adj, bool[] mark) {
            mark[x] = true;
            if (x == goal) {
                return 1;
            }
            for (int i = 0; i < adj[x].Count; ++i) {
                long child = adj[x][i];
                if (!mark[child] && dfs(child, goal, adj, mark) == 1) {
                    return 1;
                }
            }
            return 0;
        }
    }
}
