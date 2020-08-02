using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace A1 {
    public class Q4OrderOfCourse {
        static void Main(string[] args) {
            long[] t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long v = t[0];
            long e = t[1];
            long[][] edges = new long[e][];
            for (int i = 0; i < e; i++) {
                edges[i] = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            }
            long[] tt = Solve(v, edges);
            for (int i = 0; i < tt.Length; i++) {
                Console.Write(tt[i]);
                if (i != tt.Length - 1)
                    Console.Write(' ');
            }
        }
        public static long[] Solve(long nodeCount, long[][] edges) {

            //throw new NotImplementedException();
            bool[] mark = new bool[nodeCount];
            List<long>[] adj = new List<long>[nodeCount];
            List<long> ans = new List<long>();
            for (int i = 0; i < adj.Length; ++i) {
                adj[i] = new List<long>();
            }
            for (int i = 0; i < edges.Length; ++i) {
                adj[edges[i][0] - 1].Add(edges[i][1] - 1);
            }
            for (int i = 0; i < nodeCount; ++i) {
                if (!mark[i]) {
                    dfs(i, adj, mark, ans);
                }
            }
            ans.Reverse();
            return ans.ToArray();
        }

        private static void dfs(long x, List<long>[] adj, bool[] mark, List<long> ans) {
            mark[x] = true;
            for (int i = 0; i < adj[x].Count; ++i) {
                long child = adj[x][i];
                if (!mark[child]) {
                    dfs(child, adj, mark, ans);
                }
            }
            ans.Add(x + 1);
        }

       
    }
}
