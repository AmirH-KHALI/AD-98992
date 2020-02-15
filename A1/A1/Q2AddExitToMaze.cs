using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A1
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            bool[] mark = new bool[nodeCount];
            List<long>[] adj = new List<long>[nodeCount];
            for (int i = 0; i < adj.Length; ++i) {
                adj[i] = new List<long>();
            }
            for (int i = 0; i < edges.Length; ++i) {
                adj[edges[i][0] - 1].Add(edges[i][1] - 1);
                adj[edges[i][1] - 1].Add(edges[i][0] - 1);
            }
            long ans = 0;
            for (int i = 0; i < nodeCount; ++i) {
                if (!mark[i]) {
                    bfs(i, adj, mark);
                    ans++;
                }
            }
            return ans;
        }

        private void bfs(long x, List<long>[] adj, bool[] mark) {
            
            Queue<long> q = new Queue<long>();
            q.Enqueue(x);
            mark[x] = true;
            while (q.Count != 0) {
                x = q.Dequeue();
                for (int i = 0; i < adj[x].Count; ++i) {
                    long child = adj[x][i];
                    if (!mark[child]) {
                        q.Enqueue(child);
                        mark[child] = true;
                    }
                }
            }
        }
    }
}
