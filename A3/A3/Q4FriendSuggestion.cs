using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using Priority_Queue;

namespace A3
{
    public class Q4FriendSuggestion:Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) {
            //ExcludeTestCaseRangeInclusive(31, 50);
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

            long[] distStart = new long[nodeCount];
            long[] distEnd = new long[nodeCount];

            SimplePriorityQueue<long> minHeapStart = new SimplePriorityQueue<long>();
            SimplePriorityQueue<long> minHeapEnd = new SimplePriorityQueue<long>();


            List<long> markStart = new List<long>();
            List<long> markEnd = new List<long>();


            for (int i = 0; i < queries.Length; ++i) {
                queries[i][0]--;
                queries[i][1]--;
                ans[i] = Dijkstra(queries[i][0],
                                  queries[i][1],
                                  adj,
                                  radj,
                                  nodeCount,
                                  distStart,
                                  distEnd,
                                  minHeapStart,
                                  minHeapEnd,
                                  markStart,
                                  markEnd);
            }
            return ans;
        }
        private long Dijkstra(long startNode, long endNode, 
                            List<Tuple<long, long>>[] adj, List<Tuple<long, long>>[] radj, long nodeCount,
                            long[] distStart, long[] distEnd,
                            SimplePriorityQueue<long> minHeapStart, SimplePriorityQueue<long> minHeapEnd,
                            List<long> markStart, List<long> markEnd) {

            minHeapStart.Clear();
            minHeapEnd.Clear();
            markStart.Clear();
            markEnd.Clear();

            for (int i = 0; i < nodeCount; ++i) {
                distEnd[i] = int.MaxValue;
                distStart[i] = int.MaxValue;
                if (i == endNode) distEnd[i] = 0;
                if (i == startNode) distStart[i] = 0;
                minHeapEnd.Enqueue(i, distEnd[i]);
                minHeapStart.Enqueue(i, distStart[i]);
            }

            while (minHeapStart.Count > 0 && minHeapEnd.Count > 0) {
                
                long current = minHeapStart.Dequeue();
                markStart.Add(current);
                for (long i = 0; i < adj[current].Count; ++i) {
                    Tuple<long, long> child = adj[current][(int)i];
                    if (distStart[child.Item1] > distStart[current] + child.Item2) {
                        distStart[child.Item1] = distStart[current] + child.Item2;
                        minHeapStart.UpdatePriority(child.Item1, distStart[child.Item1]);
                    }
                }

                if (markEnd.Contains(current)) break;

                current = minHeapEnd.Dequeue();
                markEnd.Add(current);
                for (long i = 0; i < radj[current].Count; ++i) {
                    Tuple<long, long> child = radj[current][(int)i];
                    if (distEnd[child.Item1] > distEnd[current] + child.Item2) {
                        distEnd[child.Item1] = distEnd[current] + child.Item2;
                        minHeapEnd.UpdatePriority(child.Item1, distEnd[child.Item1]);
                    }
                }

                if (markStart.Contains(current)) break;
            }

            long ans = int.MaxValue;
            for (int i = 0; i < markStart.Count; ++i) {
                long j = markStart[i];
                if(distStart[j] < int.MaxValue && distEnd[j] < int.MaxValue &&
                    distStart[j] + distEnd[j] < ans) {
                    ans = distStart[j] + distEnd[j];
                }
            }
            for (int i = 0; i < markEnd.Count; ++i) {
                long j = markEnd[i];
                if (distStart[j] < int.MaxValue && distEnd[j] < int.MaxValue &&
                    distStart[j] + distEnd[j] < ans) {
                    ans = distStart[j] + distEnd[j];
                }
            }
            if (ans == int.MaxValue) ans = -1;
            return ans;
        }
    }
}
