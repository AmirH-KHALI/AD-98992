using System;
using System.Collections.Generic;
using System.Text;
using TestCommon;

namespace A10
{
    public class Q2CleaningApartment : Processor
    {
        public Q2CleaningApartment(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

        public String[] Solve(int V, int E, long[,] matrix)
        {
            List<String> ans = new List<String>();
            List<long>[] adj = new List<long>[V];
            for (int i = 0; i < adj.Length; ++i) {
                adj[i] = new List<long>();
            }
            for (int i = 0; i < V; ++i) {
                StringBuilder atLeast = new StringBuilder();
                for (int j = 0; j < V; ++j) {
                    atLeast.Append((i * V + j + 1) + " ");
                    for (int k = j + 1; k < V; ++k) {
                        ans.Add((-i * V - j - 1) + " " + (-i * V - k - 1) + " 0");
                        ans.Add((-j * V - i - 1) + " " + (-k * V - i - 1) + " 0");
                    }
                }
                atLeast.Append("0");
                ans.Add(atLeast.ToString());
            }
            for (int i = 0; i < E; ++i) {
                long u = matrix[i, 0] - 1;
                long v = matrix[i, 1] - 1;
                adj[u].Add(v);
                adj[v].Add(u);
            }
            for (int i = 0; i < V; ++i) { 
                for (int j = 0; j < V - 1; ++j) {
                    StringBuilder temp = new StringBuilder();
                    temp.Append((-i * V - j - 1) + " ");
                    for (int k = 0; k < adj[i].Count; ++k) {
                        long child = adj[i][k];
                        temp.Append((child * V + j + 2) + " ");
                    }
                    temp.Append("0");
                    ans.Add(temp.ToString());
                }
            }
            List<String> ansi = new List<String>();
            ansi.Add(ans.Count + " " + V * V);
            foreach (String s in ans) ansi.Add(s);
            return ansi.ToArray();
        }

    }
}
