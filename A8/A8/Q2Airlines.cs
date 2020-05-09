using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q2Airlines : Processor
    {
        public Q2Airlines(string testDataName) : base(testDataName) {
            ExcludeTestCaseRangeInclusive(1, 3);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long[]>)Solve);

        public virtual long[] Solve(long flightCount, long crewCount, long[][] info)
        {
            long n = flightCount + crewCount + 2;
            long[,] graph = new long[n, n];
            for (int i = 0; i < flightCount; ++i) {
                for (int j = 0; j < crewCount; ++j) {
                    graph[i, flightCount + j] = info[i][j];
                    graph[flightCount + j, n - 1] = 1;
                }
                graph[n - 2, i] = 1;
            }
            long[] par = new long[n];
            for (int i = 0; i < n; ++i) par[i] = -1;
            long[] ans = new long[flightCount];
            for (int i = 0; i < flightCount; ++i) ans[i] = -1;

            while (bfs(n - 2, n - 1, graph, par)) {
                long currentFlow = long.MaxValue;

                long currentNode = n - 1;
                while (currentNode != n - 2) {
                    currentFlow = Math.Min(currentFlow, graph[par[currentNode], currentNode]);
                    currentNode = par[currentNode];
                }

                currentNode = n - 1;
                while (currentNode != n - 2) {
                    graph[par[currentNode], currentNode] -= currentFlow;
                    graph[currentNode, par[currentNode]] += currentFlow;
                    currentNode = par[currentNode];
                }

            }
            for (int i = 0; i < flightCount; ++i) {
                for (int j = 0; j < crewCount; ++j) {
                    if (graph[flightCount + j, i] == 1) ans[i] = j + 1;
                }
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
