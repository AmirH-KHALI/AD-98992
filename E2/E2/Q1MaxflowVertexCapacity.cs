using System;
using System.Collections.Generic;
using TestCommon;

namespace E2
{
    public class Q1MaxflowVertexCapacity : Processor
    {
        public Q1MaxflowVertexCapacity(string testDataName) : base(testDataName)
        { /*ExcludeTestCaseRangeInclusive(1, 4);*/ }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][],long[] , long, long, long>)Solve);

        public virtual long Solve(long nodeCount, 
            long edgeCount, long[][] edges, long[] nodeWeight, 
            long startNode , long endNode)
        {
            long[,] graph = new long[nodeCount, nodeCount];
            for (int i = 0; i < edgeCount; ++i) {
                graph[--edges[i][0], --edges[i][1]] = edges[i][2];
            }
            long[] nodeFlow = new long[nodeCount];
            return getMaxFlow(graph, --startNode, --endNode, nodeFlow, nodeWeight);
        }

        private long getMaxFlow(long[,] graph, long s, long t, long[] nodeFlow, long[] nodeWeight) {
            long[] par = new long[graph.GetLength(0)];
            for (int i = 0; i < par.Length; ++i) par[i] = -1;
            long ans = 0;

            while (nodeFlow[s] < nodeWeight[s] && bfs(s, t, graph, par, nodeFlow, nodeWeight)) {
                long currentFlow = long.MaxValue;

                long currentNode = t;
                while (currentNode != s) {
                    currentFlow = Math.Min(currentFlow, nodeWeight[currentNode] - nodeFlow[currentNode]);
                    currentFlow = Math.Min(currentFlow, graph[par[currentNode], currentNode]);
                    currentNode = par[currentNode];
                }
                currentFlow = Math.Min(currentFlow, nodeWeight[currentNode] - nodeFlow[currentNode]);

                currentNode = t;
                while (currentNode != s) {
                    nodeFlow[currentNode] += currentFlow;
                    graph[par[currentNode], currentNode] -= currentFlow;
                    graph[currentNode, par[currentNode]] += currentFlow;
                    currentNode = par[currentNode];
                }
                nodeFlow[currentNode] += currentFlow;

                ans += currentFlow;
            }
            return ans;
        }

        private bool bfs(long s, long t, long[,] graph, long[] par, long[] nodeFlow, long[] nodeWeight) {

            int n = graph.GetLength(0);

            bool[] mark = new bool[n];
            Queue<long> q = new Queue<long>();
            q.Enqueue(s);
            mark[s] = true;

            while (q.Count != 0) {
                long u = q.Dequeue();

                if (u == t) break;

                for (int i = 0; i < n; ++i) {
                    if (!mark[i] && graph[u, i] > 0 && nodeFlow[i] < nodeWeight[i]) {
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
