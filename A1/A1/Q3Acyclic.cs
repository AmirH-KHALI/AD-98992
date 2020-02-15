using System;
using System.Collections.Generic;
using TestCommon;

namespace A1
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges) {
            long clock = 1;
            long[] preOrder = new long[nodeCount];
            long[] postOrder = new long[nodeCount];
            bool[] mark = new bool[nodeCount];
            List<long>[] adj = new List<long>[nodeCount];
            for (int i = 0; i < adj.Length; ++i) {
                adj[i] = new List<long>();
            }
            for (int i = 0; i < edges.Length; ++i) {
                adj[edges[i][0] - 1].Add(edges[i][1] - 1);
                //adj[edges[i][1] - 1].Add(edges[i][0] - 1);
            }
            for (int i = 0; i < nodeCount; ++i) {
                if (!mark[i]) {
                    if (dfs(i, adj, mark, clock, preOrder, postOrder)) {
                        return 1;
                    }
                }
            }
            return 0;
        }
        private bool dfs(long x, List<long>[] adj, bool[] mark,
                         long clock, long[] preOrder, long[] postOrder) {
            mark[x] = true;
            preOrder[x] = clock;
            clock++;
            for (int i = 0; i < adj[x].Count; ++i) {
                long child = adj[x][i];
                if (!mark[child]) {
                    if (dfs(child, adj, mark, clock, preOrder, postOrder))
                        return true;
                } else {
                    if (preOrder[child] != 0 && postOrder[child] == 0) {
                        return true;
                    }
                }
            }
            postOrder[x] = clock;
            clock++;
            return false;
        }
    }
}