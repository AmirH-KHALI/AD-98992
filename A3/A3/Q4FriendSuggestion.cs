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
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long,long[][], long[]>)Solve);

        public long[] Solve(long nodeCount, long edgeCount, 
                              long[][] edges, long queriesCount, 
                              long[][]queries) {
            
            long[] ans = new long[queries.Length];
            long[,] dist = new long[nodeCount, nodeCount];
            for (int i = 0; i < nodeCount; ++i) {
                for (int j = 0; j < nodeCount; ++j) {
                    dist[i, j] = int.MaxValue;
                    if (i == j) dist[i, j] = 0;
                }
            }

            for (int i = 0; i < edges.Length; ++i) {
                edges[i][0]--;
                edges[i][1]--;
                dist[edges[i][0], edges[i][1]] = edges[i][2];
            }

            for (int i = 0; i < nodeCount; ++i) {
                for (int j = 0; j < nodeCount; ++j) {
                    for (int k = 0; k < nodeCount; ++k) {
                        if (dist[j, k] > dist[j, i] + dist[i, k]) {
                            dist[j, k] = dist[j, i] + dist[i, k];
                        }
                    }
                }
            }
            
            for (int i = 0; i < queries.Length; ++i) {
                queries[i][0]--;
                queries[i][1]--;
                ans[i] = dist[queries[i][0], queries[i][1]];
            }
            return ans;
        }
    }
}
