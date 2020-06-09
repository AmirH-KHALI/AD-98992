using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A11
{
    public class Q2FunParty : Processor
    {
        public Q2FunParty(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long,long[],long[][], long>)Solve);

        List<long>[] adj;
        long[] bashe;
        long[] nabashe;
        long[] val;
        bool[] mark;

        public virtual long Solve(long n, long [] funFactors, long[][] hierarchy)
        {
            adj = new List<long>[n];
            for (int i = 0; i < n; ++i) adj[i] = new List<long>();
            bashe = new long[n];
            nabashe = new long[n];
            mark = new bool[n];
            val = funFactors;
            for (int i = 0; i < n - 1; ++i) {
                long u = hierarchy[i][0] - 1;
                long v = hierarchy[i][1] - 1;
                adj[u].Add(v);
                adj[v].Add(u);
            }
            long ans = 0;
            for (long i = 0; i < n; ++i) {
                if (!mark[i]) {
                    dfs(i);
                    ans += Math.Max(bashe[i], nabashe[i]);
                }
            }
            return ans;
        }

        private void dfs(long x) {
            mark[x] = true;

            for (int i = 0; i < adj[x].Count; ++i) {
                long child = adj[x][i];
                if (!mark[child]) {
                    dfs(child);
                    bashe[x] += nabashe[child];
                    nabashe[x] += Math.Max(bashe[child], nabashe[child]);
                }
            }
            bashe[x] += val[x];
        }
    }
}
