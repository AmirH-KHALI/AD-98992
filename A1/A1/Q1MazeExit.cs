using System;
using System.Collections.Generic;
using TestCommon;
using System.Linq;
using Microsoft.SolverFoundation.Solvers;

namespace A1
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode) {
            bool[] mark = new bool[nodeCount];
            List<long>[] adj = new List<long>[nodeCount];
            for (int i = 0; i < adj.Length; ++i) {
                adj[i] = new List<long>();
            }
            for (int i = 0; i < edges.Length; ++i) {
                adj[edges[i][0] - 1].Add(edges[i][1] - 1);
                adj[edges[i][1] - 1].Add(edges[i][0] - 1);
            }
            return dfs(StartNode - 1, EndNode - 1, adj, mark);
        }

        private long dfs(long x, long goal, List<long>[] adj, bool[] mark) {
            mark[x] = true;
            if (x == goal) {
                return 1;
            }
            for (int i = 0; i < adj[x].Count; ++i) {
                long child = adj[x][i];
                if (!mark[child] && dfs(child, goal, adj, mark) == 1) {
                    return 1;
                }
            }
            return 0;
        }
    }
}
