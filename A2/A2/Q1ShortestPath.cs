using System;
using System.Collections.Generic;
using TestCommon;

namespace A2
{
    public class Q1ShortestPath : Processor
    {
        public Q1ShortestPath(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long[][], long, long, long>)Solve);
        
        public long Solve(long NodeCount, long[][] edges, 
                          long StartNode,  long EndNode)
        {
            List<long>[] adj = new List<long>[NodeCount];
            for (int i = 0; i < NodeCount; ++i) {
                adj[i] = new List<long>();
            }

            for (int i = 0; i < edges.Length; ++i) {
                adj[edges[i][0] - 1].Add(edges[i][1] - 1);
                adj[edges[i][1] - 1].Add(edges[i][0] - 1);
            }

            bool[] mark = new bool[NodeCount];
            long[] height = new long[NodeCount];
            for (int i = 0; i < height.Length; ++i) {
                height[i] = -1;
            }
            return bfs(StartNode - 1, EndNode - 1, adj, mark, height);
        }

        private long bfs(long startNode, long endNode, List<long>[] adj, bool[] mark, long[] height) {
            
            Queue<long> q = new Queue<long>();
            mark[startNode] = true;
            height[startNode] = 0;
            q.Enqueue(startNode);
            while(q.Count > 0) {
                long current = q.Dequeue();
                for (int i = 0; i < adj[current].Count; ++i) {
                    long child = adj[current][i];
                    if (!mark[child]) {
                        mark[child] = true;
                        height[child] = height[current] + 1;
                        q.Enqueue(child);
                    }
                }
            }
            return height[endNode];
        }
    }
}
