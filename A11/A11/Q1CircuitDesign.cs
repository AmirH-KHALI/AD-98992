using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;
using Microsoft.SolverFoundation.Solvers;

namespace A11
{
    public class Q1CircuitDesign : Processor
    {
        public Q1CircuitDesign(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, 
                (Func<long, long, long[][], Tuple<bool, long[]>>)Solve);

        public override Action<string, string> Verifier =>
            TestTools.SatAssignmentVerifier;

        public long getIndex (long x) {
            if (x > 0) {
                return 2 * (x - 1);
            }
            return 2 * (-x - 1) + 1;
        }
        public long getNeg (long x) {
            if (x % 2 == 0) {
                return x + 1;
            }
            return x - 1;
        }

        public long getVar (long x) {
            if (x % 2 == 0) {
                return (x / 2) + 1; 
            }
            return -(((x - 1) / 2) + 1);
        }

        List<long>[] adj;
        List<long>[] radj;
        long n;
        bool[] mark;
        bool[] rmark;
        List<long> order;
        List<List<long>> comps;
        long[] comp;
        long[] ans;

        public virtual Tuple<bool, long[]> Solve(long v, long c, long[][] cnf)
        {
            n = 2 * v;
            adj = new List<long>[n];
            radj = new List<long>[n];
            for (int i = 0; i < n; ++i) {
                adj[i] = new List<long>();
                radj[i] = new List<long>();
            }
            mark = new bool[n];
            rmark = new bool[n];
            order = new List<long>();
            comps = new List<List<long>>();
            comp = new long[n];
            ans = new long[v];

            for (int i = 0; i < c; ++i) {
                adj[getIndex(-cnf[i][0])].Add(getIndex(cnf[i][1]));
                adj[getIndex(-cnf[i][1])].Add(getIndex(cnf[i][0]));

                radj[getIndex(cnf[i][1])].Add(getIndex(-cnf[i][0]));
                radj[getIndex(cnf[i][0])].Add(getIndex(-cnf[i][1]));
            }

            for (long i = 0; i < n; ++i) {
                if (!mark[i]) {
                    dfs(i);
                }
            }

            order.Reverse();
            long compCounter = 0;
            for (int i = 0; i < order.Count; ++i) {
                if (!rmark[order[i]]) {
                    comps.Add(new List<long>());
                    rdfs(order[i], compCounter);
                    compCounter++;
                }
            }

            for (int i = 0; i < n; ++i) {
                if (comp[i] == comp[getNeg(i)]) {
                    return new Tuple<bool, long[]>(false, new long[0]);
                }
            }

            for (long i = comps.Count - 1; i >= 0; --i) {
                for (long j = 0; j < comps[(int)i].Count; ++j) {
                    long u = comps[(int)i][(int)j];
                    if (ans[Math.Abs(getVar(u)) - 1] == 0)
                        ans[Math.Abs(getVar(u)) - 1] = getVar(u);
                }
            }
            return new Tuple<bool, long[]>(true, ans);
        }

        private void rdfs(long x, long compCounter) {
            rmark[x] = true;
            comps[(int)compCounter].Add(x);
            comp[x] = compCounter;

            for (int i = 0; i < radj[x].Count; ++i) {
                long child = radj[x][i];
                if (!rmark[child]) {
                    rdfs(child, compCounter);
                }
            }
        }

        private void dfs(long x) {
            mark[x] = true;

            for (int i = 0; i < adj[x].Count; ++i) {
                long child = adj[x][i];
                if (!mark[child]) {
                    dfs(child);
                }
            }
            order.Add(x);
        }
    }
}
