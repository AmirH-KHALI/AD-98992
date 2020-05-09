using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Q3Stocks : Processor
    {
        public Q3Stocks(string testDataName) : base(testDataName) {
            ExcludeTestCaseRangeInclusive(1, 3);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long stockCount, long pointCount, long[][] matrix)
        {
            int n = (int)stockCount;
            List<List<long>> adj = new List<List<long>>();
            for (int k = 0; k < pointCount; ++k) {
                for (int i = 0; i < n; ++i) {
                    adj.Add(new List<long>());
                }
            }

            for (int k = 0; k < pointCount; ++k) {
                DisjointUnionSets disjointUnionSets = new DisjointUnionSets(n);
                for (int i = 0; i < n; ++i) {
                    for (int j = i + 1; j < n; ++j) {
                        if (matrix[i][k] == matrix[j][k]) {
                            disjointUnionSets.union(i, j);
                        }
                        else if (k != 0 && 
                            ((matrix[i][k] > matrix[j][k]) ^ (matrix[i][k - 1] > matrix[j][k - 1]))) {
                            disjointUnionSets.union(i, j);
                        }
                    }
                }
                bool[] mark = new bool[n];
                for (int i = 0; i < n; ++i) {
                    if (mark[i]) continue;
                    int rootX = disjointUnionSets.find(i);
                    adj.Add(new List<long>());
                    adj.Add(new List<long>());
                    adj[adj.Count - 2].Add(adj.Count - 1);
                    adj[n * k + i].Add(adj.Count - 2);
                    if (k < pointCount - 1) adj[adj.Count - 1].Add(n * (k + 1) + i);
                    for (int j = i + 1; j < n; ++j) {
                        if (rootX == disjointUnionSets.find(j)) {
                            adj[n * k + j].Add(adj.Count - 2);
                            if (k < pointCount - 1) adj[adj.Count - 1].Add(n * (k + 1) + j);
                        }
                    }
                }
                if (k == pointCount - 1) {
                    adj.Add(new List<long>());
                    for (int i = 0; i < disjointUnionSets.num; ++i) {
                        adj[adj.Count - i - 2].Add(adj.Count - 1);
                    }
                    adj.Add(new List<long>());
                    for (int i = 0; i < n; ++i) {
                        adj[adj.Count - 1].Add(i);
                    }
                }
            }
            long[,] graph = new long[adj.Count, adj.Count];
            for (int i = 0; i < adj.Count; ++i) {
                for (int j = 0; j < adj[i].Count; ++j) {
                    long c = adj[i][j];
                    graph[i, c] = 1;
                }
            }

            return getMaxFlow(graph, adj.Count - 1, adj.Count - 2);
        }

        class DisjointUnionSets {
            int[] rank, parent;
            int n;
            public long num;

            // Constructor 
            public DisjointUnionSets(int n) {
                rank = new int[n];
                parent = new int[n];
                this.n = n;
                num = n;
                makeSet();
            }

            public void makeSet() {
                for (int i = 0; i < n; i++) {
                    parent[i] = i;
                }
            }
            public int find(int x) {
                if (parent[x] != x) {

                    parent[x] = find(parent[x]);

                }
                return parent[x];
            }

            public void union(int x, int y) {
                int xRoot = find(x), yRoot = find(y);

                if (xRoot == yRoot)
                    return;

                num--;

                if (rank[xRoot] < rank[yRoot])

                    parent[xRoot] = yRoot;

                else if (rank[yRoot] < rank[xRoot])

                    parent[yRoot] = xRoot;

                else 
                {
                    parent[yRoot] = xRoot;

                    rank[xRoot] = rank[xRoot] + 1;
                }
            }
        }

        private long getMaxFlow(long[,] graph, long s, long t) {
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
