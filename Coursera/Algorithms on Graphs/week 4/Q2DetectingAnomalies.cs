using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3 {
    public class Q2DetectingAnomalies {

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

        public static long[] dist;

        public static long Solve(long nodeCount, long[][] edges) {
            for (int i = 0; i < edges.Length; ++i) {
                edges[i][0]--;
                edges[i][1]--;
            }
            long[] dist = new long[nodeCount];
            for (int i = 0; i < nodeCount; ++i) {
                dist[i] = int.MaxValue;
            }
            for (int k = 0; k < nodeCount; ++k) {
                if (dist[k] != int.MaxValue) continue;
                dist[k] = 0;
                for (int i = 0; i < nodeCount - 1; ++i) {
                    for (int j = 0; j < edges.Length; ++j) {
                        if (dist[edges[j][1]] > dist[edges[j][0]] + edges[j][2]) {
                            dist[edges[j][1]] = dist[edges[j][0]] + edges[j][2];
                        }
                    }
                }

                for (int j = 0; j < edges.Length; ++j) {
                    if (dist[edges[j][1]] > dist[edges[j][0]] + edges[j][2]) {
                        return 1;
                    }
                }
            }

            return 0;
        }
    }
}
