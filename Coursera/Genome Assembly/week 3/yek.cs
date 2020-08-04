using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A8 {
    public class Q3Stocks {
        static void Main(string[] args) {

            string[] nm = Console.ReadLine().Split();
            
            long n = int.Parse(nm[0]) + 2;
            long[,] graph = new long[n, n];
            long[,] lowerKeeper = new long[n, n];
            long[,] edges = new long[int.Parse(nm[1]), 4];

            long totalLower = 0;

            for (int i = 0; i < int.Parse(nm[1]); ++i) {
                int u, v, lower, upper;
                string[] temp = Console.ReadLine().Split();
                u = int.Parse(temp[0]);
                v = int.Parse(temp[1]);
                lower = int.Parse(temp[2]);
                upper = int.Parse(temp[3]);
                edges[i, 0] = u;
                edges[i, 1] = v;
                edges[i, 2] = lower;
                edges[i, 3] = upper;
                graph[u, v] += upper - lower;
                graph[0, v] += lower;
                graph[u, n - 1] += lower;
                totalLower += lower;
            }
           
            long maxFlow = getMaxFlow(graph, 0, n - 1);

            if (maxFlow == totalLower) {
                Console.WriteLine("YES");
                for (int i = 0; i < int.Parse(nm[1]); ++i) {
                    if (graph[edges[i, 1], edges[i, 0]] > edges[i, 3] - edges[i, 2]) {
                        Console.WriteLine(edges[i, 3]);
                        graph[edges[i, 1], edges[i, 0]] -= edges[i, 3] - edges[i, 2];
                    } else {
                        Console.WriteLine(graph[edges[i, 1], edges[i, 0]] + edges[i, 2]);
                        graph[edges[i, 1], edges[i, 0]] = 0;
                    }
                }
            } else {
                Console.WriteLine("NO");
            }
        }

        private static long getMaxFlow(long[,] graph, long s, long t) {
            long[] par = new long[graph.GetLength(0)];
            for (int i = 0; i < par.Length; ++i) par[i] = -1;
            long ans = 0;

            while (bfs(s, t, graph, par)) {
                long currentFlow = long.MaxValue;

                long currentNode = t;
                while (currentNode != s) {
                    currentFlow = Math.Min(currentFlow, graph[par[currentNode], currentNode]);
                    currentNode = par[currentNode];
                }

                currentNode = t;
                while (currentNode != s) {
                    graph[par[currentNode], currentNode] -= currentFlow;
                    graph[currentNode, par[currentNode]] += currentFlow;
                    currentNode = par[currentNode];
                }

                ans += currentFlow;
            }
            return ans;
        }

        private static bool bfs(long s, long t, long[,] graph, long[] par) {

            int n = graph.GetLength(0);

            bool[] mark = new bool[n];
            Queue<long> q = new Queue<long>();
            q.Enqueue(s);
            mark[s] = true;

            while (q.Count != 0) {
                long u = q.Dequeue();

                if (u == t) break;

                for (int i = 0; i < n; ++i) {
                    if (!mark[i] && graph[u, i] > 0) {
                        mark[i] = true;
                        par[i] = u;
                        q.Enqueue(i);
                    }
                }
            }

            return mark[t];
        }
    }
}
