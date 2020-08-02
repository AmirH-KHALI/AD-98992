using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3 {
    public class Q3ExchangingMoney {
        static void Main(string[] args) {
            long[] t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long v = t[0];
            long e = t[1];
            long[][] edges = new long[e][];
            for (int i = 0; i < e; i++) {
                edges[i] = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            }
            t = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            string[] res = (Solve(v, edges, t[0]));
            foreach (var omid in res)
                Console.WriteLine(omid);
        }
        public static string[] Solve(long nodeCount, long[][] edges, long startNode) {
            for (int i = 0; i < edges.Length; ++i) {
                edges[i][0]--;
                edges[i][1]--;
            }

            long[] dist = new long[nodeCount];
            string[] ans = new string[nodeCount];

            for (int i = 0; i < nodeCount; ++i) {
                dist[i] = int.MaxValue;
            }

            dist[startNode - 1] = 0;

            for (int i = 0; i < nodeCount - 1; ++i) {
                for (int j = 0; j < edges.Length; ++j) {
                    if (dist[edges[j][0]] != int.MaxValue &&
                        dist[edges[j][1]] > dist[edges[j][0]] + edges[j][2]) {
                        dist[edges[j][1]] = dist[edges[j][0]] + edges[j][2];
                    }
                }
            }

            long[] distCpy = new long[nodeCount];
            for (int i = 0; i < nodeCount; ++i) {
                distCpy[i] = dist[i];
            }

            for (int i = 0; i < nodeCount - 1; ++i) {
                for (int j = 0; j < edges.Length; ++j) {
                    if (dist[edges[j][0]] != int.MaxValue &&
                        dist[edges[j][1]] > dist[edges[j][0]] + edges[j][2]) {
                        dist[edges[j][1]] = dist[edges[j][0]] + edges[j][2];
                    }
                }
            }

            for (int i = 0; i < nodeCount; ++i) {
                if (dist[i] == int.MaxValue) ans[i] = "*";
                else if (dist[i] != distCpy[i]) ans[i] = "-";
                else ans[i] = dist[i].ToString();
            }

            return ans;
        }
    }
}
