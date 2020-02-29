using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q2DetectingAnomalies:Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long[] dist;

        public long Solve(long nodeCount, long[][] edges)
        {
            for (int i = 0; i < edges.Length; ++i) {
                edges[i][0]--;
                edges[i][1]--;
            }
            long[] dist = new long[nodeCount];
            for (int i = 0; i < nodeCount; ++i) {
                dist[i] = int.MaxValue;
            }
            for (int k = 0; k < nodeCount; ++k) {
                if (dist[k] != int.MaxValue) continue;
                dist[k] = 0;
                for (int i = 0; i < nodeCount - 1; ++i) {
                    for (int j = 0; j < edges.Length; ++j) {
                        if (dist[edges[j][1]] > dist[edges[j][0]] + edges[j][2]) {
                            dist[edges[j][1]] = dist[edges[j][0]] + edges[j][2];
                        }
                    }
                }

                for (int j = 0; j < edges.Length; ++j) {
                    if (dist[edges[j][1]] > dist[edges[j][0]] + edges[j][2]) {
                        return 1;
                    }
                }
            }

            return 0;
        }
    }
}
