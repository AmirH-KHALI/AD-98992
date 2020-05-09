using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q1Evaquating : Processor
    {
        public Q1Evaquating(string testDataName) : base(testDataName) {
            //ExcludeTestCaseRangeInclusive(1, 2);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long nodeCount, long edgeCount, long[][] edges)
        {
            long[,] graph = new long[nodeCount, nodeCount];
            for (int i = 0; i < edgeCount; ++i) {
                graph[edges[i][0] - 1, edges[i][1] - 1] += edges[i][2];
            }

            return getMaxFlow(graph, 0, nodeCount - 1);
            
        }

        private long getMaxFlow (long[, ] graph, long s, long t) {
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

        private bool bfs(long s, long t, long[,] graph, long[] par) {

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
