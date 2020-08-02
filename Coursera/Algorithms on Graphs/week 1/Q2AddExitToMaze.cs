using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A1 {
    public class Q2AddExitToMaze {
        static void Main(string[] args) {
            long[] t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long v = t[0];
            long e = t[1];
            long[][] edges = new long[e][];
            for (int i = 0; i < e; i++) {
                edges[i] = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            }
            Console.WriteLine(Solve(v, edges));
        }
        public static long Solve(long nodeCount, long[][] edges) {
            bool[] mark = new bool[nodeCount];
            List<long>[] adj = new List<long>[nodeCount];
            for (int i = 0; i < adj.Length; ++i) {
                adj[i] = new List<long>();
            }
            for (int i = 0; i < edges.Length; ++i) {
                adj[edges[i][0] - 1].Add(edges[i][1] - 1);
                adj[edges[i][1] - 1].Add(edges[i][0] - 1);
            }
            long ans = 0;
            for (int i = 0; i < nodeCount; ++i) {
                if (!mark[i]) {
                    bfs(i, adj, mark);
                    ans++;
                }
            }
            return ans;
        }

        private static void bfs(long x, List<long>[] adj, bool[] mark) {

            Queue<long> q = new Queue<long>();
            q.Enqueue(x);
            mark[x] = true;
            while (q.Count != 0) {
                x = q.Dequeue();
                for (int i = 0; i < adj[x].Count; ++i) {
                    long child = adj[x][i];
                    if (!mark[child]) {
                        q.Enqueue(child);
                        mark[child] = true;
                    }
                }
            }
        }
    }
}
