using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Q1FrequencyAssignment : Processor
    {
        public Q1FrequencyAssignment(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);


        public String[] Solve(int V, int E, long[,] matrix)
        {
            List<String> ans = new List<String>();
            ans.Add((4 * V + 3 * E) + " " + (3 * V));
            for (int i = 0; i < V; ++i) {
                ans.Add((3 * i + 1) + " " + (3 * i + 2) + " " + (3 * i + 3) + " 0");
                ans.Add((-3 * i - 1) + " " + (-3 * i - 2) + " 0");
                ans.Add((-3 * i - 1) + " " + (-3 * i - 3) + " 0");
                ans.Add((-3 * i - 2) + " " + (-3 * i - 3) + " 0");
            }
            for (int i = 0; i < E; ++i) {
                long u = matrix[i, 0] - 1;
                long v = matrix[i, 1] - 1;
                ans.Add((-3 * u - 1) + " " + (-3 * v - 1) + " 0");
                ans.Add((-3 * u - 2) + " " + (-3 * v - 2) + " 0");
                ans.Add((-3 * u - 3) + " " + (-3 * v - 3) + " 0");
            }
            return ans.ToArray();
        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

    }
}
