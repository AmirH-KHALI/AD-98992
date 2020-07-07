using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8 {
    public class Q3Stocks : Processor {
        public Q3Stocks(string testDataName) : base(testDataName) {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long stockCount, long pointCount, long[][] matrix) {
            long n = stockCount * 2 + 2;
            long[,] graph = new long[n, n];

            long[][] info = new long[stockCount][];
            for (int i = 0; i < stockCount; i++) info[i] = new long[stockCount];

            for (int i = 0; i < stockCount; ++i) {
                for (int j = 0; j < stockCount; ++j) {
                    if (i != j) {
                        info[i][j] = check(matrix[i], matrix[j]);
                    }
                }
            }

            for (int i = 0; i < stockCount; ++i) {
                for (int j = 0; j < stockCount; ++j) {
                    graph[i, stockCount + j] = info[i][j];
                    graph[stockCount + j, n - 1] = 1;
                }
                graph[n - 2, i] = 1;
            }

            return stockCount - getMaxFlow(graph, n - 2, n - 1);
        }

        private long check(long[] a, long[] b) {

            for (int i = 0; i < a.Length; ++i) {
                if (a[i] <= b[i]) {
                    return 0;
                }
            }

            return 1;
        }

        private long getMaxFlow(long[,] graph, long s, long t) {
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
