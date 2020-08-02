using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A8 {
    public class Q1Evaquating {

        static void Main(string[] args) {
            long[] node = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            long nodeCount = node[0];
            long edgeCount = node[1];
            long[][] edges = new long[edgeCount][];
            for (int i = 0; i < edgeCount; i++)
                edges[i] = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
            Console.WriteLine(Solve(nodeCount, edgeCount, edges));
        }

        public static long Solve(long nodeCount, long edgeCount, long[][] edges) {
            long[,] graph = new long[nodeCount, nodeCount];
            for (int i = 0; i < edgeCount; ++i) {
                graph[edges[i][0] - 1, edges[i][1] - 1] += edges[i][2];
            }

            return getMaxFlow(graph, 0, nodeCount - 1);

        }

        private static long getMaxFlow(long[,] graph, long s, long t) {
            long[] par = new long[graph.GetLength(0)];
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
