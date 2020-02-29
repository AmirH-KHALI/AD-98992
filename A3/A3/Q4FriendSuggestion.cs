using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3
{
    public class Q4FriendSuggestion:Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) {

            ExcludeTestCaseRangeInclusive(1, 48);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long,long[][], long[]>)Solve);

        public long[] Solve(long nodeCount, long edgeCount, 
                              long[][] edges, long queriesCount, 
                              long[][] queries) {
            
            long[] ans = new long[queries.Length];
            List<Tuple<long, long>>[] adj = new List<Tuple<long, long>>[nodeCount];
            List<Tuple<long, long>>[] radj = new List<Tuple<long, long>>[nodeCount];
            for (int i = 0; i < nodeCount; ++i) {
                adj[i] = new List<Tuple<long, long>>();
                radj[i] = new List<Tuple<long, long>>();
            }

            for (int i = 0; i < edges.Length; ++i) {
                adj[edges[i][0] - 1].Add(new Tuple<long, long>(edges[i][1] - 1, edges[i][2]));
                radj[edges[i][1] - 1].Add(new Tuple<long, long>(edges[i][0] - 1, edges[i][2]));
            }

            for (int i = 0; i < queries.Length; ++i) {
                queries[i][0]--;
                queries[i][1]--;
                ans[i] = dijkstra(queries[i][0], queries[i][1], adj, radj, nodeCount);
            }
            return ans;
        }
        private long dijkstra(long startNode, long endNode, List<Tuple<long, long>>[] adj, 
                              List<Tuple<long, long>>[] radj, long nodeCount) {

            long[] distStart = new long[nodeCount];
            long[] distEnd = new long[nodeCount];
            SortedSet<Tuple<long, long>> minHeapStart = new SortedSet<Tuple<long, long>>();
            SortedSet<Tuple<long, long>> minHeapEnd = new SortedSet<Tuple<long, long>>();

            bool[] markStart = new bool[nodeCount];
            bool[] markEnd = new bool[nodeCount];

            for (int i = 0; i < nodeCount; ++i) {
                distEnd[i] = int.MaxValue;
                distStart[i] = int.MaxValue;
                if (i == endNode) distEnd[i] = 0;
                if (i == startNode) distStart[i] = 0;
                minHeapEnd.Add(new Tuple<long, long>(distEnd[i], i));
                minHeapStart.Add(new Tuple<long, long>(distStart[i], i));
            }

            while (minHeapStart.Count > 0 && minHeapEnd.Count > 0) {
                
                Tuple<long, long> current = minHeapStart.Min();
                markStart[current.Item2] = true;
                minHeapStart.Remove(minHeapStart.Min());
                for (long i = 0; i < adj[current.Item2].Count; ++i) {
                    Tuple<long, long> child = adj[current.Item2][(int)i];
                    if (distStart[child.Item1] > current.Item1 + child.Item2) {
                        minHeapStart.Remove(new Tuple<long, long>(distStart[child.Item1], child.Item1));
                        distStart[child.Item1] = current.Item1 + child.Item2;
                        minHeapStart.Add(new Tuple<long, long>(distStart[child.Item1], child.Item1));
                    }
                }

                if (markEnd[current.Item2]) break;

                current = minHeapEnd.Min();
                markEnd[current.Item2] = true;
                minHeapEnd.Remove(minHeapEnd.Min());
                for (long i = 0; i < radj[current.Item2].Count; ++i) {
                    Tuple<long, long> child = radj[current.Item2][(int)i];
                    if (distEnd[child.Item1] > current.Item1 + child.Item2) {
                        minHeapEnd.Remove(new Tuple<long, long>(distEnd[child.Item1], child.Item1));
                        distEnd[child.Item1] = current.Item1 + child.Item2;
                        minHeapEnd.Add(new Tuple<long, long>(distEnd[child.Item1], child.Item1));
                    }
                }

                if (markStart[current.Item2]) break;
            }

            long ans = int.MaxValue;
            for (int i = 0; i < nodeCount; ++i) {
                if(distStart[i] < int.MaxValue && distEnd[i] < int.MaxValue &&
                    distStart[i] + distEnd[i] < ans) {
                    ans = distStart[i] + distEnd[i];
                }
            }
            if (ans == int.MaxValue) ans = -1;
            return ans;
        }
    }
}
